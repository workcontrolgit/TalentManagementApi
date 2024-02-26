using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TalentManagementApi.Application.Interfaces;
using TalentManagementApi.Application.Parameters;
using TalentManagementApi.Application.Wrappers;

namespace TalentManagementApi.Application.Features.Prompts.Queries.GetPrompts
{
    /// <summary>
    /// GetAllPromptsQuery - handles media IRequest BaseRequestParameter - contains paging
    /// parameters To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetPromptsQuery : ListParameter, IRequest<Response<GetPromptViewModel>>
    {
        public string Prompt { get; set; }

        public class GetPromptQueryHandler : IRequestHandler<GetPromptsQuery, Response<GetPromptViewModel>>
        {
            private readonly IPromptService _repository;

            /// <summary>
            /// Constructor for GetPromptQueryHandler class.
            /// </summary>
            /// <param name="repository">IPromptRepositoryAsync object.</param>
            /// <param name="modelHelper">IModelHelper object.</param>
            /// <returns>GetPromptQueryHandler object.</returns>
            public GetPromptQueryHandler(IPromptService repository)
            {
                _repository = repository;
            }

            /// <summary>
            /// Handles the GetPromptsQuery request and returns a PagedResponse containing the requested data.
            /// </summary>
            /// <param name="request">The GetPromptsQuery request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A PagedResponse containing the requested data.</returns>
            public async Task<Response<GetPromptViewModel>> Handle(GetPromptsQuery request, CancellationToken cancellationToken)
            {
                var completion = await _repository.CreateChatCompletionAsync(request.Prompt);
                var response = new GetPromptViewModel();
                response.Completion = completion;
                return new Response<GetPromptViewModel>(response);
            }
        }
    }
}