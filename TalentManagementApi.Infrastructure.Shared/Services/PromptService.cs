using Microsoft.Extensions.Configuration;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System.Threading.Tasks;
using TalentManagementApi.Application.Interfaces;

namespace PromptAPI.Services
{
    public class PromptService : IPromptService
    {
        public readonly IConfiguration _configuration;

        public PromptService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateChatCompletionAsync(string prompt)
        {
            var apiKey = _configuration.GetValue<string>("OpenAISetting:APIKey");

            // https://github.com/OkGoDoIt/OpenAI-API-dotnet/blob/master/README.md
            OpenAIAPI api = new OpenAIAPI(apiKey); // shorthand

            var result = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                Model = Model.ChatGPTTurbo,
                Temperature = 0.7f,
                MaxTokens = 100,
                Messages = new ChatMessage[] {
            new ChatMessage(ChatMessageRole.User, prompt)
                }
            });

            var reply = result.Choices[0].Message.TextContent;

            return reply;
        }
    }
}