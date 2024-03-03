using Nest;
using TalentManagementApi.Application.Interfaces.Repositories;
using TalentManagementApi.Domain.Entities;

namespace TalentManagementApi.Infrastructure.Persistence.Repositories
{
    public class EmployeeDocumentAsync : GenericDocumentAsync<Employee>, IEmployeeDocumentAsync
    {
        public EmployeeDocumentAsync(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}