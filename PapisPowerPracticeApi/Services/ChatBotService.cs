using Azure.AI.OpenAI;
using Azure;
using OpenAI.Chat;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Services
{
    public class ChatBotService : IChatBotService
    {
        private readonly AzureOpenAIClient _openAIClient;
        private readonly string _deploymentName;

        public ChatBotService(IConfiguration configuration)
        {
            var endpoint = configuration["AzureOpenAI:Endpoint"];
            var apiKey = configuration["AzureOpenAI:ApiKey"];
            _deploymentName = configuration["AzureOpenAI:DeploymentName"];
            
            _openAIClient = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
        }

        public async Task<string> GetResponseAsync(string message)
        {
            try
            {
                var chatClient = _openAIClient.GetChatClient(_deploymentName);
                var response = await chatClient.CompleteChatAsync(new[]
                {
                    new UserChatMessage(message)
                });

                return response.Value.Content[0].Text;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}