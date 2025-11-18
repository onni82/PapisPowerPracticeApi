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

            // Hittar eller skapar session
            ChatSession session;
            if (request.SessionId.HasValue) {
                session = await _context.ChatSessions
                    .Include(s => s.Messages)
                    .FirstOrDefaultAsync(s => s.Id == request.SessionId.Value && s.UserId == userId)
                    ?? throw new KeyNotFoundException("Chat session not found");
            } else {
                session = new ChatSession
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    Title = GenerateTitleFromMessage(request.Message),
                    Messages = new List<ChatMessage>()
                };
                _context.ChatSessions.Add(session);
            }
        }

        private static string GenerateTitleFromMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "New training session";
            }
            var trimmed = message.Length <= 40 ? message : message[..40];
            return $"Session: {trimmed}";
        }

        //public async Task<string> GetResponseAsync(string message)
        //{
        //    try
        //    {
        //        var chatClient = _openAIClient.GetChatClient(_deploymentName);
        //        var response = await chatClient.CompleteChatAsync(new[]
        //        {
        //            new UserChatMessage(message)
        //        });

        //        return response.Value.Content[0].Text;
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"Error: {ex.Message}";
        //    }
        //}
    }
}