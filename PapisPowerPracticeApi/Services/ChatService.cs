using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Services
{
    public class ChatService : IChatService
    {
        private readonly AppDbContext _context;
        private readonly ChatClient _chatClient;

        public ChatService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;

            var apiKey = configuration["OpenAI:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("OpenAI API key is missing in configuration.");

            // Skapa klient för GPT-modell
            _chatClient = new ChatClient("gpt-4o-mini", apiKey);
        }

        public async Task<string> GetAiResponseAsync(string userId, string message)
        {
            // Spara användarens meddelande
            var userMsg = new PapisPowerPracticeApi.Models.ChatMessage
            {
                UserId = userId,
                Role = "user",
                Message = message,
                Timestamp = DateTime.UtcNow
            };
            _context.ChatMessages.Add(userMsg);
            await _context.SaveChangesAsync();

            // Hämta historik
            var history = await _context.ChatMessages
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.Timestamp)
                .Take(10)
                .ToListAsync();

            // Bygg upp OpenAI-meddelanden
            var chatMessages = new List<ChatMessage>();

            foreach (var m in history.OrderBy(m => m.Timestamp))
            {
                var role = m.Role == "assistant" ? ChatRole.Assistant : ChatRole.User;
                chatMessages.Add(new ChatMessage(role, m.Message!));
            }

            // Lägg till det nya användarmeddelandet sist
            chatMessages.Add(new ChatMessage(ChatRole.User, message));

            // Skicka till OpenAI
            var response = await _chatClient.CompleteChatAsync(chatMessages);

            var aiText = response.Content[0].Text ?? "Tyvärr, jag kunde inte generera ett svar just nu.";

            // Spara AI-svar
            var aiMsg = new PapisPowerPracticeApi.Models.ChatMessage
            {
                UserId = userId,
                Role = "assistant",
                Message = aiText,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(aiMsg);
            await _context.SaveChangesAsync();

            return aiText;
        }

        public async Task<IEnumerable<PapisPowerPracticeApi.Models.ChatMessage>> GetChatHistoryAsync(string userId, int limit = 20)
        {
            return await _context.ChatMessages
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.Timestamp)
                .Take(limit)
                .ToListAsync();
        }
    }
}
