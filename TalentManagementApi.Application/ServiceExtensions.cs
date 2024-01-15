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
            services.AddScoped<IDataShapeHelper<Position>, DataShapeHelper<Position>>();
            services.AddScoped<IDataShapeHelper<Employee>, DataShapeHelper<Employee>>();
            services.AddScoped<IModelHelper, ModelHelper>();
        }
    }
}