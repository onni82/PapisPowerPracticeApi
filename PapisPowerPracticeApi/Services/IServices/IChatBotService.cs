using PapisPowerPracticeApi.DTOs.Chat.Request;
using PapisPowerPracticeApi.DTOs.Chat.Response;

namespace PapisPowerPracticeApi.Services.IServices
{
    public interface IChatBotService
    {
        Task<ChatMessageDTO> SendMessageAsync(string userId, ChatRequestDTO request);
        Task<IEnumerable<ChatMessageDTO>> GetSessionMessagesAsync(Guid sessionId, string userId);
        Task<IEnumerable<ChatSessionDTO>> GetUserSessionsAsync(string userId);
    }
}