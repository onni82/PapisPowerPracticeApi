using Azure.AI.OpenAI;
using Azure;
using OpenAI.Chat;
using PapisPowerPracticeApi.Services.IServices;
using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.DTOs.Chat.Response;
using PapisPowerPracticeApi.DTOs.Chat.Request;
using PapisPowerPracticeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace PapisPowerPracticeApi.Services
{
    public class ChatBotService : IChatBotService
    {
        private readonly AzureOpenAIClient _openAIClient;
        private readonly string _deploymentName;
        private readonly AppDbContext _context;

        public ChatBotService(IConfiguration configuration, AppDbContext context)
        {
            var endpoint = configuration["AzureOpenAI:Endpoint"];
            var apiKey = configuration["AzureOpenAI:ApiKey"];
            _deploymentName = configuration["AzureOpenAI:DeploymentName"];

            _openAIClient = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
            _context = context;
        }

        public async Task<ChatMsgDTO> SendMessageAsync(string userId, ChatRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("Invalid user id", nameof(userId));
            }

            // Save user's message (no sessions)
            var userMessageEntity = new ChatMsg
            {
                UserId = userId,
                Message = request.Message,
                IsUserMessage = true,
                Timestamp = DateTime.UtcNow
            };
            _context.ChatMessages.Add(userMessageEntity);
            await _context.SaveChangesAsync();

            // Build conversation context: all messages for the user ordered by timestamp
            var history = await _context.ChatMessages
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            var chatMessages = new List<ChatMessage>
            {
                new SystemChatMessage("Du är en personlig träningscoach. Hjälp användaren att skapa en personlig träningsplan baserad på deras mål, erfarenhetsnivå och preferenser. Ställ förtydligande frågor vid behov och ge handlingsbara, progressiva planer (set, repetitioner, vila, återhämtningstips). Håll svaren vänliga och koncisa.")
            };

            foreach (var m in history)
            {
                chatMessages.Add(m.IsUserMessage ? new UserChatMessage(m.Message) : new AssistantChatMessage(m.Message));
            }

            try
            {
                var chatClient = _openAIClient.GetChatClient(_deploymentName);
                var response = await chatClient.CompleteChatAsync(chatMessages.ToArray());

                var assistantText = response.Value.Content[0].Text ?? string.Empty;

                // Save assistant message
                var assistantMessageEntity = new ChatMsg
                {
                    UserId = userId,
                    Message = assistantText,
                    IsUserMessage = false,
                    Timestamp = DateTime.UtcNow
                };
                _context.ChatMessages.Add(assistantMessageEntity);
                await _context.SaveChangesAsync();

                return new ChatMsgDTO
                {
                    Id = assistantMessageEntity.Id,
                    Message = assistantMessageEntity.Message,
                    IsUserMessage = assistantMessageEntity.IsUserMessage,
                    Timestamp = assistantMessageEntity.Timestamp
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"OpenAI call failed: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<ChatMsgDTO>> GetUserMessagesAsync(string userId)
        {
            var messages = await _context.ChatMessages
                .AsNoTracking()
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.Timestamp)
                .Select(m => new ChatMsgDTO
                {
                    Id = m.Id,
                    Message = m.Message,
                    IsUserMessage = m.IsUserMessage,
                    Timestamp = m.Timestamp
                })
                .ToListAsync();

            return messages;
        }
    }
}