using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Services
{
    public class ChatService : IChatService
    {
        private readonly AppDbContext _context;
        private readonly AzureOpenAIClient _azureClient;
        private readonly ChatClient _chatClient;

        public ChatService(AppDbContext context)
        {
            _context = context;

            // Skapa klient för GPT-modell
            _azureClient = new AzureOpenAIClient(
                new Uri("https://your-azure-openai-resource.com"),
                new DefaultAzureCredential());
            _chatClient = _azureClient.GetChatClient("my-gpt-4o-mini-deployment");
        }

        //public async Task<string> GetAiResponseAsync(string userId, string message)
        //{
        //    return "";
        //}

        //public async Task<IEnumerable<ChatMsg>> GetChatHistoryAsync(string userId, int limit = 20)
        //{
        //    return await _context.ChatMsgs
        //        .Where(m => m.UserId == userId)
        //        .OrderByDescending(m => m.Timestamp)
        //        .Take(limit)
        //        .ToListAsync();
        //}
    }
}
