using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services
{
    public class RabbitMQWorker : BackgroundService
    {
        public IServiceProvider Services { get; }

        public RabbitMQWorker(IServiceProvider services)
        {
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = Services.CreateScope();
            var messageBusConsumer =
                scope.ServiceProvider
                    .GetRequiredService<IMessageBusConsumer>();

            while (!stoppingToken.IsCancellationRequested)
            {
                await messageBusConsumer.SetupMessageConsumersAsync();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
