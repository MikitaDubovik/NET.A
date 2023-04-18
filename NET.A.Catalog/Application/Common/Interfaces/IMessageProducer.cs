namespace Application.Common.Interfaces
{
    public interface IMessageProducer : IDisposable
    {
        void SendMessage<T>(T message);
    }
}
