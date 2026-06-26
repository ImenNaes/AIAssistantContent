
namespace Infrastructure.Config
{
    public class AzureAIOptions
    {
        public string? ENDPOINT_OPENAI_FOUNDRY { get; set; }
        public string? KEYVAULT_URI  { get; set; }
        public string? MODEL_OPENAI_FOUNDRY { get; set; }
    }
}
