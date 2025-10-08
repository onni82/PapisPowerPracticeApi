using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Repositories.IRepositories
{
    public interface IChatRepository
    {
        Task<IEnumerable<ChatMessage>> GetChatHistoryAsync(string userId);
        Task AddMessageAsync(ChatMessage message);
        Task SaveChangesAsync();
    }
}
