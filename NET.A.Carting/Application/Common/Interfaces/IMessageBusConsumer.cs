namespace Application.Common.Interfaces
{
    public interface IMessageBusConsumer
    {
        Task ReceiveMessage<T>(string queueName);

        Task SetupMessageConsumersAsync();

        void Dispose();
    }
}
