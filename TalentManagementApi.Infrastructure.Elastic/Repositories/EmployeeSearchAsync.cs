using Nest;
using TalentManagementApi.Application.Interfaces.Repositories;
using TalentManagementApi.Domain.Entities;

namespace TalentManagementApi.Infrastructure.Search.Repositories
{
    public class EmployeeSearchAsync : GenericSearchAsync<Employee>, IEmployeeSearchAsync
    {
        public EmployeeSearchAsync(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}