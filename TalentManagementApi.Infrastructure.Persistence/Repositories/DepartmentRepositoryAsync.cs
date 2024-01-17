using TalentManagementApi.Application.Interfaces.Repositories;
using TalentManagementApi.Domain.Entities;
using TalentManagementApi.Infrastructure.Persistence.Contexts;
using TalentManagementApi.Infrastructure.Persistence.Repository;

namespace TalentManagementApi.Infrastructure.Persistence.Repositories
{
    public class DepartmentRepositoryAsync : GenericRepositoryAsync<Department>, IDepartmentRepositoryAsync
    {
        public DepartmentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}