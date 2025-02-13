using System.Diagnostics.CodeAnalysis;
using Flight.Services.Services;
using Flight.Services.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Flight.Services.Ioc;

[ExcludeFromCodeCoverage]
public static class ServicesIoc
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IRoutesService, RoutesService>();
        return services;
    }
}