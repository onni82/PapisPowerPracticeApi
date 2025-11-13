namespace PapisPowerPracticeApi.Services.IServices
{
    public interface IChatBotService
    {
        Task<string> GetResponseAsync(string message);
    }
}