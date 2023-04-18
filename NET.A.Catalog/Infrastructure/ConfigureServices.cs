using Application.Common.Interfaces;
using Infrastructure.MessageBus.Senders;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton(serviceProvider => new ConnectionFactory
        {
            HostName = "localhost"
        });

        services.AddScoped<IApplicationDbContextInitialiser, ApplicationDbContextInitialiser>();
        services.AddScoped<IMessageProducer, RabbitMQProducer>();

        services.AddTransient<IDateTime, DateTimeService>();

        services.AddSingleton<IAuditableEntitySaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();
        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
