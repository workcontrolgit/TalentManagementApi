using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using System.Threading.Tasks;
using TalentManagementApi.Domain.Entities;

namespace TalentManagementApi.Infrastructure.Shared.Services
{
    public class SearchService : ISearchService
    {
        public readonly IConfiguration _configuration;
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<SearchService> _logger;

        public SearchService(IConfiguration configuration, IElasticClient elasticClient, ILogger<SearchService> logger)
        {
            _configuration = configuration;
            _elasticClient = elasticClient;
            _logger = logger;
        }

        /// <summary>
        /// Search asynchronously using the specified parameters.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation. The result contains the search response.
        /// </returns>
        public async Task<ISearchResponse<Employee>> SearchMultiMatchAsync(string q)
        {
            // https://www.elastic.co/guide/en/elasticsearch/client/net-api/1.x/writing-queries.html
            // https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/multi-match-usage.html
            var response = await _elasticClient.SearchAsync<Employee>(s =>
                s.Query(sq =>
                    sq.MultiMatch(mm => mm
                        .Query(q)
                        .Fuzziness(Fuzziness.Auto)
                    )
                )
            );

            if (!response.IsValid)
                _logger.LogError(response.OriginalException, "Problem searching Elasticsearch for term {0}", q);

            return response;
        }
    }
}