using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using TalentManagementApi.Application;
using TalentManagementApi.Infrastructure.Persistence;
using TalentManagementApi.Infrastructure.Persistence.Contexts;
using TalentManagementApi.Infrastructure.Persistence.SeedData;
using TalentManagementApi.Infrastructure.Shared;
using TalentManagementApi.WebApi.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);
    // load up serilog configuraton
    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
    builder.Host.UseSerilog(Log.Logger);

    Log.Information("Application startup services registration");

    builder.Services.AddApplicationLayer();
    builder.Services.AddPersistenceInfrastructure(builder.Configuration);
    builder.Services.AddSharedInfrastructure(builder.Configuration);
    // Elastic Search
    builder.Services.AddElasticsearch(builder.Configuration);
    // Swagger
    builder.Services.AddSwaggerExtension();
    builder.Services.AddControllersExtension();
    // Add CORS services to the service container
    builder.Services.AddCorsExtension();
    // Add health checks services to the service container
    builder.Services.AddHealthChecks();
    // Add JWT authentication service to the service collection
    builder.Services.AddJWTAuthentication(builder.Configuration);
    // Add authorization policies to the service collection
    builder.Services.AddAuthorizationPolicies(builder.Configuration);
    // Add API versioning to the service collection
    builder.Services.AddApiVersioningExtension();
    // Add MVC Core services to the service container
    builder.Services.AddMvcCore()
        // Add API Explorer services to the MVC Core
        .AddApiExplorer();
    // Add Versioned API Explorer Extension services to the service container
    builder.Services.AddVersionedApiExplorerExtension();

    // Building the app using the builder
    var app = builder.Build();

    Log.Information("Application startup middleware registration");

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        // for quick database (usually for prototype during development)
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            // use context
            if (dbContext.Database.EnsureCreated())
            {
                DbInitializer.SeedData(dbContext);
            }
        }
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    // Add this line; you'll need `using Serilog;` up the top, too
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseRouting();
    //Enable CORS
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwaggerExtension();
    app.UseErrorHandlingMiddleware();
    app.UseHealthChecks("/health");
    app.MapControllers();

    Log.Information("Application Starting");

    app.Run();
}
catch (Exception ex)
{
    Log.Warning(ex, "An error occurred starting the application");
}
finally
{
    Log.CloseAndFlush();
}