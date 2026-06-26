using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Config;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using OpenAI.Chat;

namespace Infrastructure.Services
{
    public class OpenAIService: IOpenAIService
    {
        private readonly AzureAIOptions _options;
        private readonly AzureOpenAIClient _client;
        public OpenAIService(IOptions<AzureAIOptions> options)
        {
            _options = options.Value;
            _client = new AzureOpenAIClient(
                   new Uri(_options.ENDPOINT_OPENAI_FOUNDRY!),
                   new AzureKeyCredential(_options.KEYVAULT_URI!)
            );
        }

        public async Task<string> GenerateChatAsync(ChatRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var chatClient = _client.GetChatClient(_options.MODEL_OPENAI_FOUNDRY);

                var messages = new List<ChatMessage>
                {
                    new UserChatMessage(request.Prompt)
                };

                var response = await chatClient.CompleteChatAsync(
                    messages,
                    new ChatCompletionOptions
                    {
                        Temperature = 0.7f,
                        FrequencyPenalty = 0f,
                        PresencePenalty = 0f
                    }
                );

                var chatResponse = response.Value.Content.Last().Text;
                return chatResponse;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
