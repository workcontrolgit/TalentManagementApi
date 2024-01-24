using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TalentManagementApi.Application.Interfaces;
using TalentManagementApi.Application.Interfaces.Repositories;
using TalentManagementApi.Infrastructure.Persistence.Contexts;
using TalentManagementApi.Infrastructure.Persistence.Repositories;
using TalentManagementApi.Infrastructure.Persistence.Repository;

namespace TalentManagementApi.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            #region Repositories

            // * Code to manually register repositories for DI
            //services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            //services.AddTransient<IPositionRepositoryAsync, PositionRepositoryAsync>();
            //services.AddTransient<IEmployeeRepositoryAsync, EmployeeRepositoryAsync>();
            //services.AddTransient<IDepartmentRepositoryAsync, DepartmentRepositoryAsync>();
            //services.AddTransient<ISalaryRangeRepositoryAsync, SalaryRangeRepositoryAsync>();

            // * use Scutor to register generic repository interface for DI and specifying the lifetime of dependencies
            services.Scan(selector => selector
                .FromCallingAssembly()
                .AddClasses(classSelector => classSelector.AssignableTo(typeof(IGenericRepositoryAsync<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                );

            #endregion Repositories
        }
    }
}