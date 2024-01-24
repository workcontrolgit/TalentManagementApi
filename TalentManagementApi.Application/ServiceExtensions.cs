using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TalentManagementApi.Application.Behaviours;
using TalentManagementApi.Application.Helpers;
using TalentManagementApi.Application.Interfaces;
using TalentManagementApi.Domain.Entities;
using System.Reflection;

namespace TalentManagementApi.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IModelHelper, ModelHelper>();

            // * Code to manually register repositories for DI
            //services.AddScoped<IDataShapeHelper<Position>, DataShapeHelper<Position>>();
            //services.AddScoped<IDataShapeHelper<Employee>, DataShapeHelper<Employee>>();
            //services.AddScoped<IDataShapeHelper<Department>, DataShapeHelper<Department>>();
            //services.AddScoped<IDataShapeHelper<SalaryRange>, DataShapeHelper<SalaryRange>>();

            // * use Scutor to register generic IDataShapeHelper interface for DI and specifying the lifetime of dependencies
            services.Scan(selector => selector
                .FromCallingAssembly()
                .AddClasses(classSelector => classSelector.AssignableTo(typeof(IDataShapeHelper<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        }
    }
}