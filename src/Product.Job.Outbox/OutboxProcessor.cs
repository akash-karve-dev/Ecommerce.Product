using AutoMapper;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Product.Application.Contracts;
using Product.Infra.Cosmos;
using Product.Job.Outbox.Models.Incoming;
using Product.Job.Outbox.Models.IntegrationEvents;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Product.Job.Outbox
{
    public class OutboxProcessor
    {
        private readonly IMessageBroker _messageBroker;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly IMapper _mapper;
        private readonly IDbProvider _dbProvider;
        private readonly ILogger<OutboxProcessor> _logger;

        public OutboxProcessor(
            IMessageBroker messageBroker,
            JsonSerializerOptions jsonSerializerOptions,
            IMapper mapper,
            IDbProvider dbProvider,
            ILogger<OutboxProcessor> logger)
        {
            _messageBroker = messageBroker;
            _jsonSerializerOptions = jsonSerializerOptions;
            _mapper = mapper;
            _dbProvider = dbProvider;
            _logger = logger;
        }

        [FunctionName("ProductOutboxProcessor")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "ProductsDb",
            collectionName: "Products",
            ConnectionStringSetting = "DbConnectionString",
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists = true )]
            IReadOnlyList<Document> input)
        {
            try
            {
                /*
                 * TODO : RETRY MECHANISM
                 * 1. We can use Polly for retry
                 * 2. Push message to DLQ
                 * 3. NOTE: Azure SDK's also has built-in retry mechanism.
                 */

                var container = _dbProvider.GetDatabase().GetContainer("Products");

                foreach (var document in input)
                {
                    var outboxEvent = JsonSerializer.Deserialize<BaseOutboxEvent>(document.ToString(), _jsonSerializerOptions);

                    // Entity other than "Outbox entity"
                    if (outboxEvent == null)
                    {
                        await Task.CompletedTask;
                        return;
                    }

                    //Outbox entity
                    var outboxEntityEvent = outboxEvent as OutboxEntityEvent;

                    if (!outboxEntityEvent.IsProcessed)
                    {
                        var productIntegrationEvent = _mapper.Map<ProductIntegrationEvent>(outboxEvent);

                        await _messageBroker.PublishAsync(productIntegrationEvent);

                        var patchOperations = new[]
                        {
                        PatchOperation.Add("/isProcessed", true),
                        PatchOperation.Add("/ttl", 60*5) // 5 minutes TTL
                    };

                        await container.PatchItemAsync<OutboxEntityEvent>(
                            outboxEntityEvent.Id.ToString(),
                            new Microsoft.Azure.Cosmos.PartitionKey(outboxEntityEvent.PartitionKey),
                            patchOperations);

                        _logger.LogInformation(message:
                            "Outbox entity with eventId:[{eventId}] and eventType: [{eventType}] processed successfully",
                            outboxEntityEvent.Id,
                            outboxEntityEvent.EventType);
                    }
                }
            }
            catch (System.Exception ex)
            {
                // TODO : HANDLE DLQ
                _logger.LogError(ex, ex.Message);

                /*
                 * As a best practice, you should catch all exceptions in your code and rethrow any errors that you want to result in a retry.
                 * This will trigger retry of azure function
                 *
                 * Event Hubs checkpoints won't be written until the retry policy for the execution has finished. Because of this behavior, progress on the specific partition is paused until the current batch has finished.
                 */
                throw;
            }
        }
    }
}