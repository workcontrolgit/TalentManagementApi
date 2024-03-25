using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TalentManagementApi.Application.Interfaces.Repositories;

namespace TalentManagementApi.Infrastructure.Search.Repositories
{
    public class GenericSearchAsync<T> : IGenericSearchAsync<T> where T : class
    {
        private readonly IElasticClient _elasticClient;

        public GenericSearchAsync(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<IndexResponse> AddAsync(T entity)
        {
            return await _elasticClient.IndexDocumentAsync(entity);
        }

        public async Task<UpdateResponse<T>> UpdateAsync(T entity)
        {
            return await _elasticClient.UpdateAsync<T>(entity, u => u.Doc(entity));
        }

        public async Task<DeleteResponse> DeleteAsync(Guid id)
        {
            return await _elasticClient.DeleteAsync<T>(id);
        }

        public async Task<ISearchResponse<T>> GetAllAsync()
        {
            return await _elasticClient.SearchAsync<T>(s => s
                .Query(q => q
                .MatchAll()
                ));
        }

        public async Task<GetResponse<T>> GetByIdAsync(Guid id)
        {
            return await _elasticClient.GetAsync<T>(id);
        }

        public async Task<BulkResponse> BulkAsync(IEnumerable<T> entities)
        {
            return await _elasticClient.IndexManyAsync(entities);
        }

        public async Task<ISearchResponse<T>> SearchAsync(string q)
        {
            return await _elasticClient.SearchAsync<T>(s =>
                s.Query(sq =>
                    sq.MultiMatch(mm => mm
                        .Query(q)
                        .Fuzziness(Fuzziness.Auto)
                    )
                ));
        }
    }
}