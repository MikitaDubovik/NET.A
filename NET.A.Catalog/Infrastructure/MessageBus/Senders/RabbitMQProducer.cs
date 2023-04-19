using Application.Common.Interfaces;
using Application.Items.Commands.UpdateItem;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.MessageBus.Senders
{
    public class RabbitMQProducer : IMessageProducer
    {
        private const string UpdateItemQueue = "update-item";

        private IConnection _connection;
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMQProducer(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            ConnectToRabbitMq();
        }

        public void SendMessage<T>(T message)
        {
            var queueName = GetQueueName(typeof(T));

            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
        }

        private string GetQueueName(Type messageType) =>
            messageType switch
            {
                Type when messageType == typeof(UpdateItemCommand) => UpdateItemQueue,
                _ => throw new KeyNotFoundException($"There is no queue for the type {messageType}")
            };

        private void ConnectToRabbitMq()
        {
            if (_connection == null || _connection.IsOpen == false)
            {
                _connection = _connectionFactory.CreateConnection();
            }
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
    }
}
