using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TalentManagementApi.Application.Features.Searches.Queries.GetSearches;

namespace TalentManagementApi.WebApi.Controllers.v1
{
    public class SearchController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> CreateChatCompletionAsync([FromQuery] string q)
        {
            return Ok(await Mediator.Send(new GetSearchesQuery { Search = q }));
        }
    }
}