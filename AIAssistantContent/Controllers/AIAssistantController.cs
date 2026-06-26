using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AIAssistantContent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIAssistantController : ControllerBase
    {
        public readonly IOpenAIService _openAIService;
        private readonly ILogger<AIAssistantController> _logger;
        public AIAssistantController(IOpenAIService openAIService, ILogger<AIAssistantController> logger)
        {
            _openAIService = openAIService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> post([FromBody] ChatRequest request)
        {
            _logger.LogInformation("Requête reçue avec prompt : {Prompt}", request);
            try
            {
                var domainRequest = new ChatRequest
                {
                    Prompt = request.Prompt
                };

                var response = await _openAIService.GenerateChatAsync(domainRequest);
                _logger.LogInformation("Réponse générée avec succès");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération du contenu.");
                return await Task.FromResult<IActionResult>(BadRequest(ex.Message));
            }
        }
    }
}
