namespace Product.Infra.Cosmos.Models
{
    public class OutboxEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? PartitionKey { get; set; }
        public string? EventType { get; set; }
        public string? EventPayload { get; set; }
        public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public bool? IsProcessed { get; set; } = false;

        public string? Discriminator { get; set; } = "OutboxEntity";
    }
}