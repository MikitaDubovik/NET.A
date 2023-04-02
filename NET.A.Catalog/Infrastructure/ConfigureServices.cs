using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDbContextInitialiser, ApplicationDbContextInitialiser>();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddSingleton<IAuditableEntitySaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();
        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
