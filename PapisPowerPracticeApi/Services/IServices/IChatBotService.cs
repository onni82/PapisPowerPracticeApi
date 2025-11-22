using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PapisPowerPracticeApi.DTOs.Chat.Request;
using PapisPowerPracticeApi.DTOs.Chat.Response;

namespace PapisPowerPracticeApi.Services.IServices
{
    public interface IChatBotService
    {
        Task<ChatMsgDTO> SendMessageAsync(string userId, ChatRequestDTO request);
        Task<IEnumerable<ChatMsgDTO>> GetUserMessagesAsync(string userId);
    }
}