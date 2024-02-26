using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TalentManagementApi.Application.Features.Prompts.Queries.GetPrompts;
using TalentManagementApi.Application.Interfaces;

namespace TalentManagementApi.WebApi.Controllers.v1
{
    public class PromptController : BaseApiController
    {
        private readonly IPromptService _promptService;

        public PromptController(IPromptService promptService)
        {
            _promptService = promptService;
        }

        [HttpGet(Name = "CreateChatCompletionAsync")]
        public async Task<IActionResult> TriggerOpenAI([FromQuery] string prompt)
        {
            return Ok(await Mediator.Send(new GetPromptsQuery { Prompt = prompt }));
        }
    }
}