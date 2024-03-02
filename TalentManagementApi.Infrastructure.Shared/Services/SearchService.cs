using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using System.Threading.Tasks;
using TalentManagementApi.Domain.Entities;

namespace TalentManagementApi.Infrastructure.Shared.Services
{
    public class SearchService
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
        /// Creates a chat completion asynchronously using the specified parameters.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation. The result contains the chat completion.
        /// </returns>
        public async Task<ISearchResponse<Employee>> CreateChatCompletionAsync(string q)
        {
            // Good article if you want to read an overview of the types of
            // queries available: https://qbox.io/blog/elasticsearch-queries-match-phrase-match
            // Specific usage for MultiMatch: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/multi-match-usage.html
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