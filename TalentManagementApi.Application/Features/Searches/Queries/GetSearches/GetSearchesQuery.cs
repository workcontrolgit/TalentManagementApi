using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TalentManagementApi.Application.Interfaces.Repositories;
using TalentManagementApi.Domain.Entities;

namespace TalentManagementApi.Application.Features.Searches.Queries.GetSearches
{
    /// <summary>
    /// GetSearchQuery - handles media IRequest BaseRequestParameter
    /// </summary>
    public class GetSearchesQuery : IRequest<IEnumerable<Employee>>
    {
        public string Search { get; set; }

        public class GetSearchQueryHandler : IRequestHandler<GetSearchesQuery, IEnumerable<Employee>>
        {
            private readonly IEmployeeSearchAsync _searchService;

            /// <summary>
            /// Constructor for GetSearchQueryHandler class.
            /// </summary>
            /// <param name="repository">ISearchRepositoryAsync object.</param>
            /// <param name="modelHelper">IModelHelper object.</param>
            /// <returns>GetSearchQueryHandler object.</returns>
            public GetSearchQueryHandler(IEmployeeSearchAsync repository)
            {
                _searchService = repository;
            }

            /// <summary>
            /// Handles the GetSearchQuery request and returns a PagedResponse containing the requested data.
            /// </summary>
            /// <param name="request">The GetSearchQuery request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A PagedResponse containing the requested data.</returns>
            public async Task<IEnumerable<Employee>> Handle(GetSearchesQuery request, CancellationToken cancellationToken)
            {
                var completion = await _searchService.SearchAsync(request.Search);
                return completion.Documents;
            }
        }
    }
}