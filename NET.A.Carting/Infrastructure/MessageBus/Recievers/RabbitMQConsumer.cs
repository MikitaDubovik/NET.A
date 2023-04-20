using Application.Common.Interfaces;
using Application.Items.Commands.UpdateItem;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infrastructure.MessageBus.Recievers
{
    public class RabbitMQConsumer : IMessageBusConsumer
    {
        private const string UpdateItemQueue = "update-item";

        private IConnection _connection;
        private readonly ConnectionFactory _connectionFactory;

        private readonly IMediator _mediator;

        public RabbitMQConsumer(IMediator mediator, ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            ConnectToRabbitMq();
            _mediator = mediator;
        }

        public Task ReceiveMessage<T>(string queueName)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, eventArgs) => await OnEventReceived<T>(sender, eventArgs);
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        public async Task SetupMessageConsumersAsync()
        {
            await ReceiveMessage<UpdateItemCommand>(UpdateItemQueue);
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }

        protected virtual async Task OnEventReceived<T>(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var body = Encoding.UTF8.GetString(@event.Body.ToArray());
                var message = JsonSerializer.Deserialize<T>(body);

                await _mediator.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ConnectToRabbitMq()
        {
            if (_connection == null || _connection.IsOpen == false)
            {
                _connection = _connectionFactory.CreateConnection();
            }
        }
    }
}
