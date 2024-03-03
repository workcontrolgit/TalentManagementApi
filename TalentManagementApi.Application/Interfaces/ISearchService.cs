using Nest;
using System.Threading.Tasks;
using TalentManagementApi.Domain.Entities;

namespace TalentManagementApi.Infrastructure.Shared.Services
{
    public interface ISearchService
    {
        Task<ISearchResponse<Employee>> SearchMultiMatchAsync(string q);
    }
}