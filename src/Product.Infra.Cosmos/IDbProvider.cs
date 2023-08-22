using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Product.Application;
using Product.Infra.Cosmos.Configurations;

namespace Product.Infra.Cosmos
{
    public interface IDbProvider
    {
        Database GetDatabase();
    }

    internal class CosmosDbProvider : IDbProvider
    {
        private readonly CosmosDbConfiguration _dbConfiguration;
        private CosmosClient? _client;

        public CosmosDbProvider(IOptions<CosmosDbConfiguration> options)
        {
            _dbConfiguration = options.Value;
        }

        public Database GetDatabase()
        {
            if (_client != null)
            {
                return _client.GetDatabase(_dbConfiguration.DbName);
            }

            _client = new CosmosClient(_dbConfiguration.ConnectionString,
                new CosmosClientOptions
                {
                    Serializer = new CosmosSystemTextJsonSerializer(SerializerOptions.DefaultSerializerOptions()),
                });
            return _client.GetDatabase(_dbConfiguration.DbName);
        }
    }
}