using TalentManagementApi.Application.Interfaces.Repositories;
using TalentManagementApi.Domain.Entities;
using TalentManagementApi.Infrastructure.Persistence.Contexts;
using TalentManagementApi.Infrastructure.Persistence.Repository;

namespace TalentManagementApi.Infrastructure.Persistence.Repositories
{
    public class DepartmentRepositoryAsync : GenericRepositoryAsync<Department>, IDepartmentRepositoryAsync
    {
        //private readonly DbSet<Department> _repository;

        public DepartmentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            //_repository = dbContext.Set<Department>();
        }
    }
}