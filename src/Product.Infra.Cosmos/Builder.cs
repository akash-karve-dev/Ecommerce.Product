using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Contracts;
using Product.Infra.Cosmos.Configurations;

namespace Product.Infra.Cosmos
{
    public static class Builder
    {
        public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Builder));

            services.AddOptions<CosmosDbConfiguration>()
                .Bind(configuration.GetSection(CosmosDbConfiguration.ConfigPath));

            services.AddScoped<IDbProvider, CosmosDbProvider>();

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}