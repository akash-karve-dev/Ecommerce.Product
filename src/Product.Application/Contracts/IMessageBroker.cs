using Product.Application.IntegrationEvents;

namespace Product.Application.Contracts
{
    public interface IMessageBroker
    {
        Task PublishAsync<T>(T message) where T : IIntegrationEvent;
    }
}