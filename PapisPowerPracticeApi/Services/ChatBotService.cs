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
                    Messages = new List<ChatMsg>()
                };
                _context.ChatSessions.Add(session);
            }

            // Sparar användarmeddelande
            var userMessageEntity = new ChatMsg
            {
                UserId = userId,
                Message = request.Message,
                IsUserMessage = true,
                Timestamp = DateTime.UtcNow,
                ChatSessionId = session.Id
            };
            _context.ChatMessages.Add(userMessageEntity);
            await _context.SaveChangesAsync();

            // Bygger konversation (systemefterfrågan + historia)
            var chatClient = _openAIClient.GetChatClient(_deploymentName);

            // Tar upp tidigare meddelanden för kontext (inkluderar både användar- och assistentmeddelanden, sorterade)
            var history = await _context.ChatMessages
                .Where(m => m.ChatSessionId == session.Id)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            var chatMessages = new List<ChatMessage>
            {
                new SystemChatMessage("Du är en personlig träningscoach. Hjälp användaren att skapa en personlig träningsplan baserad på deras mål, erfarenhetsnivå och preferenser. Ställ förtydligande frågor vid behov och ge handlingsbara, progressiva planer (set, repetitioner, vila, återhämtningstips). Håll svaren vänliga och koncisa.")
            };

            // Konverterar historik till ChatMessage-format
            foreach (var m in history)
            {
                chatMessages.Add(m.IsUserMessage ? new UserChatMessage(m.Message) : new AssistantChatMessage(m.Message));
            }

            try
            {
                var response = await chatClient.CompleteChatAsync(chatMessages.ToArray());

                var assistantText = response.Value.Content[0].Text ?? string.Empty;
                
                // Sparar assistentens svar
                var assistantMessageEntity = new ChatMsg
                {
                    UserId = userId,
                    Message = assistantText,
                    IsUserMessage = false,
                    Timestamp = DateTime.UtcNow,
                    ChatSessionId = session.Id
                };
                _context.ChatMessages.Add(assistantMessageEntity);
                await _context.SaveChangesAsync();

                // Ser till att sessionen kvarstod (i fall den nyss skapades)
                if (session.CreatedAt == default)
                {
                    session.CreatedAt = DateTime.UtcNow;
                    _context.ChatSessions.Update(session);
                    await _context.SaveChangesAsync();
                }

                return new ChatMsgDTO
                {
                    Id = assistantMessageEntity.Id,
                    ChatSessionId = assistantMessageEntity.ChatSessionId,
                    Message = assistantMessageEntity.Message,
                    IsUserMessage = assistantMessageEntity.IsUserMessage,
                    Timestamp = assistantMessageEntity.Timestamp
                };
            }
            catch (Exception ex)
            {
                // Logga fel (om nödvändigt)
                throw new InvalidOperationException($"OpenAI call failed: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<ChatMsgDTO>> GetSessionMessagesAsync(Guid sessionId, string userId)
        {
            var session = await _context.ChatSessions
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId);

            if (session == null)
            {
                throw new InvalidOperationException("Session not found or does not belong to the user");
            }

            var messages = await _context.ChatMessages
                .AsNoTracking()
                .Where(m => m.ChatSessionId == sessionId)
                .OrderBy(m => m.Timestamp)
                .Select(m => new ChatMsgDTO
                {
                    Id = m.Id,
                    ChatSessionId = m.ChatSessionId,
                    Message = m.Message,
                    IsUserMessage = m.IsUserMessage,
                    Timestamp = m.Timestamp
                })
                .ToListAsync();

            return messages;
        }

        public async Task<IEnumerable<ChatSessionDTO>> GetUserSessionAsync(string userId)
        {
            var sessions = await _context.ChatSessions
                .AsNoTracking()
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => new ChatSessionDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();

            return sessions;
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