using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Repositories.IRepositories;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repository;
        private readonly HttpClient _httpClient;

        public ChatService(IChatRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClient = httpClientFactory.CreateClient("OpenAI");
        }

        public async Task<string> GetAiResponseAsync(string userId, string userMessage)
        {
            // Spara användarmeddelande
            var userMsg = new ChatMessage
            {
                UserId = userId,
                Role = "user",
                Message = userMessage
            };

            await _repository.AddMessageAsync(userMsg);

            // Skicka till extern LLM (ex. OpenAI)
            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "Du är en personlig tränare. Skapa träningsscheman utifrån användarens mål." },
                    new { role = "user", content = userMessage }
                }
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<dynamic>();
            string aiResponse = result?.choices?[0]?.message?.content ?? "Kunde inte generera svar.";

            // Spara AI-svar
            var aiMsg = new ChatMessage
            {
                UserId = userId,
                Role = "assistant",
                Message = aiResponse
            };

            await _repository.AddMessageAsync(aiMsg);
            await _repository.SaveChangesAsync();

            return aiResponse;
        }
    }
}
