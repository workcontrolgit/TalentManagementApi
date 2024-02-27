using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TalentManagementApi.Application.Interfaces;
using TalentManagementApi.Application.Wrappers;

namespace TalentManagementApi.Application.Features.Prompts.Queries.GetPrompts
{
    /// <summary>
    /// GetPromptQuery - handles media IRequest BaseRequestParameter
    /// </summary>
    public class GetPromptQuery : IRequest<Response<GetPromptViewModel>>
    {
        public string Prompt { get; set; }

        public class GetPromptQueryHandler : IRequestHandler<GetPromptQuery, Response<GetPromptViewModel>>
        {
            private readonly IPromptService _promptService;

            /// <summary>
            /// Constructor for GetPromptQueryHandler class.
            /// </summary>
            /// <param name="repository">IPromptRepositoryAsync object.</param>
            /// <param name="modelHelper">IModelHelper object.</param>
            /// <returns>GetPromptQueryHandler object.</returns>
            public GetPromptQueryHandler(IPromptService repository)
            {
                _promptService = repository;
            }

            /// <summary>
            /// Handles the GetPromptQuery request and returns a PagedResponse containing the requested data.
            /// </summary>
            /// <param name="request">The GetPromptQuery request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A PagedResponse containing the requested data.</returns>
            public async Task<Response<GetPromptViewModel>> Handle(GetPromptQuery request, CancellationToken cancellationToken)
            {
                var completion = await _promptService.CreateChatCompletionAsync(request.Prompt);
                var response = new GetPromptViewModel();
                response.Completion = completion;
                return new Response<GetPromptViewModel>(response);
            }
        }
    }
}