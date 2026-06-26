using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOpenAIService
    {
        Task<string> GenerateChatAsync(ChatRequest request, CancellationToken cancellationToken = default);

    }
}
