using Microsoft.EntityFrameworkCore;
using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Repositories.IRepositories;

namespace PapisPowerPracticeApi.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChatMessage>> GetChatHistoryAsync(string userId)
        {
            return await _context.ChatMessages
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task AddMessageAsync(ChatMessage message)
        {
            await _context.ChatMessages.AddAsync(message);
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
