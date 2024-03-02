using Microsoft.Extensions.Configuration;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System.Threading.Tasks;
using TalentManagementApi.Application.Interfaces;

namespace TalentManagementApi.Infrastructure.Shared.Services
{
    public class PromptService : IPromptService
    {
        public readonly IConfiguration _configuration;

        public PromptService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a chat completion asynchronously using the specified parameters.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation. The result contains the chat completion.
        /// </returns>
        public async Task<string> CreateChatCompletionAsync(string prompt)
        {
            var apiKey = _configuration.GetValue<string>("OpenAISetting:APIKey");

            // https://github.com/OkGoDoIt/OpenAI-API-dotnet/blob/master/README.md
            OpenAIAPI api = new OpenAIAPI(apiKey); // shorthand

            //This code snippet is using an asynchronous method to create a chat completion using an API. Here's a breakdown of what's happening:
            //1. The code is calling the `CreateChatCompletionAsync` method of the `api.Chat` object. This method likely sends a request to a server to generate a chat completion based on the provided parameters.
            //2. The method is being awaited using the `await` keyword, indicating that the code will wait for the completion of this asynchronous operation before proceeding.
            //3. Inside the method call, a new `ChatRequest` object is created and initialized with the following properties:
            // - `Model`: This specifies the model to be used for generating the chat completion. In this case, it is set to `ChatGPTTurbo`.
            // - `Temperature`: This parameter controls the randomness of the generated text. A lower value will result in more predictable responses, while a higher value will introduce more randomness. Here, it is set to `0.7f`.
            // - `MaxTokens`: This specifies the maximum number of tokens (words or characters) that the generated completion can have. In this case, it is set to `100`.
            // - `Messages`: This is an array of `ChatMessage` objects. In this case, a single `ChatMessage` object is added to the array, representing a user message with the content specified by the `prompt` variable.
            //4. The `CreateChatCompletionAsync` method likely processes this `ChatRequest` object, sends it to the server, and returns a result asynchronously. The result may contain the generated chat completion based on the provided input.
            //Overall, this code snippet is a part of a process that uses an API to generate chat completions based on user input and specific parameters.
            var result = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                Model = Model.ChatGPTTurbo,
                Temperature = 0.7f,
                MaxTokens = 100,
                Messages = new ChatMessage[] {
            new ChatMessage(ChatMessageRole.User, prompt)
                }
            });

            //This line of code is accessing the `TextContent` property of the first choice in the `Choices` array of the `result` object. It is assigning the value of the `TextContent` property to the variable `reply`. This code snippet is useful for extracting and storing the text content of the first choice in a conversation or a list of choices.
            var reply = result.Choices[0].Message.TextContent;

            return reply;
        }
    }
}