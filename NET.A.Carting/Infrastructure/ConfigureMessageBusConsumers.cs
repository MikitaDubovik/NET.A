using Application.Common.Interfaces;
using Infrastructure.MessageBus.Recievers;
using Infrastructure.Services;
using RabbitMQ.Client;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureMessageBusConsumers
{
    public static IServiceCollection AddMessageBusConsumers(this IServiceCollection services)
    {
        services.AddSingleton(serviceProvider => new ConnectionFactory
        {
            HostName = "localhost"
        });

        services.AddScoped<IMessageBusConsumer, RabbitMQConsumer>();

        services.AddHostedService<RabbitMQWorker>();
        return services;
    }
}
