using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Product.Application;
using Product.Application.Contracts;
using Product.Application.IntegrationEvents;
using System.Text.Json;

namespace Product.Infra.AzureServiceBus
{
    internal sealed class AzureServiceBusMessageBroker : IMessageBroker
    {
        private readonly ServiceBusSender _serviceBusSender;
        private readonly ILogger<AzureServiceBusMessageBroker> _logger;
        private const string _topicName = "ecommerce-event-topic";

        public AzureServiceBusMessageBroker(
            IServiceBusProvider serviceBusProvider,
            ILogger<AzureServiceBusMessageBroker> logger)
        {
            _serviceBusSender = serviceBusProvider.GetServiceBusClient().CreateSender(_topicName);
            _logger = logger;
        }

        public async Task PublishAsync<T>(T message) where T : IIntegrationEvent
        {
            var payload = JsonSerializer.Serialize(message, SerializerOptions.DefaultSerializerOptions());

            var messageId = message.EventId.ToString();

            var busMessage = new ServiceBusMessage(payload)
            {
                /*
                 * Duplicate dectection is enabled on topic
                 * MessageId is use for duplication detection
                 */
                MessageId = messageId
            };
            await _serviceBusSender.SendMessageAsync(busMessage);

            _logger.LogInformation(
                "Message with Id: [{messageId}] sent on topic: [{topicName}]",
                messageId,
                _topicName);
        }
    }
}