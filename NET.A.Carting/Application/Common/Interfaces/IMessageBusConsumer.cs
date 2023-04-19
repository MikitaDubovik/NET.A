namespace Application.Common.Interfaces
{
    public interface IMessageBusConsumer : IDisposable
    {
        Task ReceiveMessage<T>(string queueName);

        Task SetupMessageConsumersAsync();

        void Dispose();
    }
}
