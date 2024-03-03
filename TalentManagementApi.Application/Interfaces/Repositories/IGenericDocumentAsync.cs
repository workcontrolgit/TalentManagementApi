using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TalentManagementApi.Application.Interfaces.Repositories
{
    public interface IGenericDocumentAsync<T> where T : class
    {
        Task<ISearchResponse<T>> GetAllAsync();

        Task<GetResponse<T>> GetByIdAsync(Guid id);

        Task<IndexResponse> AddAsync(T entity);

        Task<UpdateResponse<T>> UpdateAsync(T entity);

        Task<DeleteResponse> DeleteAsync(Guid id);

        Task<BulkResponse> BulkAsync(IEnumerable<T> entities);

        Task<ISearchResponse<T>> SearchAsync(string q);
    }
}