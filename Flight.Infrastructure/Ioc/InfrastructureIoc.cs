using System.Diagnostics.CodeAnalysis;
using Flight.Infrastructure.Context;
using Flight.Infrastructure.Repositories;
using Flight.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Flight.Infrastructure.Ioc;

[ExcludeFromCodeCoverage]
public static  class InfrastructureIoc
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IRoutesRepository, RoutesRepository>();
        return services;
    }
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FlightDB");
        if (connectionString == null)
            throw new ArgumentNullException($"ConnectionString is nullorEmpty {nameof(connectionString)}");

        services.AddDbContext<FlightContext>(options => options.UseSqlite(connectionString));
        return services;
    }
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Flight Api",
                Version = "v1",
                Description = "",
                Contact = new OpenApiContact
                {
                    Name = "raidy_silva@hotmail.com"
                }
            });
        });
        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services) =>
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
    #region Host
    public static IHost RunMigration(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<FlightContext>();
        context.Database.Migrate();
        return host;
    }
    #endregion
}