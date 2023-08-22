using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Contracts;
using Product.Infra.AzureServiceBus.Configurations;

namespace Product.Infra.AzureServiceBus
{
    public static class Builder
    {
        public static IServiceCollection AddAzureServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageBroker, AzureServiceBusMessageBroker>();
            services.AddScoped<IServiceBusProvider, ServiceBusProvider>();

            services.AddOptions<AzureServiceBusConfiguration>()
                .Bind(configuration.GetSection((AzureServiceBusConfiguration.ConfigPath)));

            return services;
        }
    }
}