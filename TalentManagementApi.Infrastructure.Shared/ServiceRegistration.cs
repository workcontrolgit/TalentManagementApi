using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
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

        public static void AddElasticsearch(this IServiceCollection services, IConfiguration _config)
        {
            var settings = new ConnectionSettings(new Uri(_config["ElasticsearchSettings:uri"]));

            var defaultIndex = _config["ElasticsearchSettings:defaultIndex"];

            if (!string.IsNullOrEmpty(defaultIndex))
                settings = settings.DefaultIndex(defaultIndex);

            // The authentication options below are set if you have non-null/empty
            // settings in the _config.  These are just samples -- there are
            // other authentication methods available.
            var apiKeyId = _config["ElasticsearchSettings:apiKeyId"];
            var apiKey = _config["ElasticsearchSettings:apiKey"];

            if (!string.IsNullOrEmpty(apiKeyId) && !string.IsNullOrEmpty(apiKey))
            {
                settings = settings.ApiKeyAuthentication(apiKeyId, apiKey);
            }
            else
            {
                var basicAuthUser = _config["ElasticsearchSettings:basicAuthUser"];
                var basicAuthPassword = _config["ElasticsearchSettings:basicAuthPassword"];

                if (!string.IsNullOrEmpty(basicAuthUser) && !string.IsNullOrEmpty(basicAuthPassword))
                    settings = settings.BasicAuthentication(basicAuthUser, basicAuthPassword);
            }

            var client = new ElasticClient(settings);

            // ElasticClient is thread-safe
            // See https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/lifetimes.html
            services.AddSingleton<IElasticClient>(client);
        }
    }
}