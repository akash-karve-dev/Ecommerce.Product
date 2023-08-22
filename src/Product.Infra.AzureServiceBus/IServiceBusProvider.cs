using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Product.Infra.AzureServiceBus.Configurations;

namespace Product.Infra.AzureServiceBus
{
    internal interface IServiceBusProvider
    {
        ServiceBusClient GetServiceBusClient();
    }

    internal class ServiceBusProvider : IServiceBusProvider
    {
        private readonly AzureServiceBusConfiguration _busConfiguration;

        public ServiceBusProvider(IOptions<AzureServiceBusConfiguration> options)
        {
            _busConfiguration = options.Value;
        }

        public ServiceBusClient GetServiceBusClient()
        {
            var client = new ServiceBusClient(_busConfiguration.ConnectionString);
            return client;
        }
    }
}