namespace PapisPowerPracticeApi.Services.IServices
{
    public interface IChatService
    {
        Task<string> GetAiResponseAsync(string userId, string userMessage);
    }
}
