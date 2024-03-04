using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TalentManagementApi.Application.Wrappers;
using TalentManagementApi.Infrastructure.Shared.Services;

namespace TalentManagementApi.Application.Features.Searches.Queries.GetSearches
{
    /// <summary>
    /// GetSearchQuery - handles media IRequest BaseRequestParameter
    /// </summary>
    public class GetSearchesQuery : IRequest<Response<GetSearchesViewModel>>
    {
        public string Search { get; set; }

        public class GetSearchQueryHandler : IRequestHandler<GetSearchesQuery, Response<GetSearchesViewModel>>
        {
            private readonly ISearchService _searchService;

            /// <summary>
            /// Constructor for GetSearchQueryHandler class.
            /// </summary>
            /// <param name="repository">ISearchRepositoryAsync object.</param>
            /// <param name="modelHelper">IModelHelper object.</param>
            /// <returns>GetSearchQueryHandler object.</returns>
            public GetSearchQueryHandler(ISearchService repository)
            {
                _searchService = repository;
            }

            /// <summary>
            /// Handles the GetSearchQuery request and returns a PagedResponse containing the requested data.
            /// </summary>
            /// <param name="request">The GetSearchQuery request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A PagedResponse containing the requested data.</returns>
            public async Task<Response<GetSearchesViewModel>> Handle(GetSearchesQuery request, CancellationToken cancellationToken)
            {
                var completion = await _searchService.SearchMultiMatchAsync(request.Search);
                var response = new GetSearchesViewModel();
                // response.Completion = completion;  TODO mapper completion to GetSearchesViewModel
                return new Response<GetSearchesViewModel>(response);
            }
        }
    }
}