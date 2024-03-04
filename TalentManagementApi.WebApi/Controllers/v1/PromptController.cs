using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TalentManagementApi.Application.Features.Prompts.Queries.GetPrompts;

namespace TalentManagementApi.WebApi.Controllers.v1
{
    public class PromptController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> CreateChatCompletionAsync([FromQuery] string prompt)
        {
            return Ok(await Mediator.Send(new GetPromptQuery { Prompt = prompt }));
        }
    }
}