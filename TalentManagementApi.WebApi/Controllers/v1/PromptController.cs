using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TalentManagementApi.Application.Interfaces;

namespace TalentManagementApi.WebApi.Controllers.v1
{
    //[ApiController]
    //[Route("[controller]")]
    public class PromptController : BaseApiController
    {
        private readonly IPromptService _promptService;

        public PromptController(IPromptService promptService)
        {
            _promptService = promptService;
        }

        [HttpGet(Name = "TriggerOpenAI")]
        public async Task<IActionResult> TriggerOpenAI([FromQuery] string input)
        {
            var response = await _promptService.TriggerOpenAI(input);
            return Ok(response);
        }
    }
}