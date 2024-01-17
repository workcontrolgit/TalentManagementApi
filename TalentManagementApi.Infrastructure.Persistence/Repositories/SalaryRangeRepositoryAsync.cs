using Microsoft.EntityFrameworkCore;
using TalentManagementApi.Application.Interfaces.Repositories;
using TalentManagementApi.Domain.Entities;
using TalentManagementApi.Infrastructure.Persistence.Contexts;
using TalentManagementApi.Infrastructure.Persistence.Repository;

namespace TalentManagementApi.Infrastructure.Persistence.Repositories
{
    public class SalaryRangeRepositoryAsync : GenericRepositoryAsync<SalaryRange>, ISalaryRangeRepositoryAsync
    {
        private readonly DbSet<SalaryRange> _repository;

        public SalaryRangeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _repository = dbContext.Set<SalaryRange>();
        }
    }
}