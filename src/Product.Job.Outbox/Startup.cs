using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application;
using Product.Infra.AzureServiceBus;
using Product.Infra.Cosmos;
using Product.Job.Outbox;
using Product.Job.Outbox.Converters;
using System.IO;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Product.Job.Outbox
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.GetContext().Configuration;

            services.AddScoped((sp) =>
            SerializerOptions.DefaultSerializerOptions(new OutboxEventJsonConverter()));

            services.AddAutoMapper(typeof(Startup));
            services.AddAzureServiceBus(configuration);
            services.AddCosmosDb(configuration);
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var path = Directory.GetCurrentDirectory();
            builder.ConfigurationBuilder
                .AddJsonFile($"{path}//appsettings.json", optional: true)
                .AddJsonFile($"{path}//appsettings.{builder.GetContext().EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            base.ConfigureAppConfiguration(builder);
        }
    }
}