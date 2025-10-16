using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Services.IServices
{
    public interface IChatService
    {
        Task<string> GetAiResponseAsync(string userId, string message);
        Task<IEnumerable<ChatMessage>> GetChatHistoryAsync(string userId, int limit = 20);
    }
}
