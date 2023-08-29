using AutoMapper;
using Microsoft.Azure.Cosmos;
using Product.Application.Contracts;
using Product.Infra.Cosmos.Models;

namespace Product.Infra.Cosmos
{
    internal class ProductRepository : IProductRepository
    {
        private const string ContainerName = "Products";
        private readonly Container _container;
        private readonly IMapper _mapper;

        public ProductRepository(
            IDbProvider dbProvider,
            IMapper mapper)
        {
            _container = dbProvider.GetDatabase().GetContainer(ContainerName);
            _mapper = mapper;
        }

        public async Task AddAsync(Domain.Models.Product product)
        {
            var partitionKey = product.Category.ToString();

            var cosmosProduct = _mapper.Map<CosmosProduct>(product);

            var outboxEntity = _mapper.Map<OutboxEntity>(product);

            var transactionalBatch = _container.CreateTransactionalBatch(new PartitionKey(partitionKey));

            transactionalBatch.CreateItem(cosmosProduct);
            transactionalBatch.CreateItem(outboxEntity);

            var transactionBatchResponse = await transactionalBatch.ExecuteAsync();

            if (!transactionBatchResponse.IsSuccessStatusCode)
            {
                // TODO: THROW CUSTOM EXCEPTION
                throw new Exception(transactionBatchResponse.ErrorMessage);
            }
        }
    }
}