using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TalentManagementApi.Application.Interfaces;
using TalentManagementApi.Domain.Settings;
using TalentManagementApi.Infrastructure.Shared.Services;

namespace TalentManagementApi.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMockService, MockService>();
        }
    }
}