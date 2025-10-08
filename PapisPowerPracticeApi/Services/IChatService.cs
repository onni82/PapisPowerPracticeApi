namespace PapisPowerPracticeApi.Services
{
    public interface IChatService
    {
        Task<string> GetAiResponseAsync(string userId, string userMessage);
    }
}
