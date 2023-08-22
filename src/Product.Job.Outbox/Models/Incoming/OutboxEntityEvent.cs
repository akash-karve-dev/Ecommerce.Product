using System;

namespace Product.Job.Outbox.Models.Incoming
{
    internal class OutboxEntityEvent : BaseOutboxEvent
    {
        public Guid Id { get; set; }
        public string PartitionKey { get; set; }
        public string EventType { get; set; }
        public string EventPayload { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public bool IsProcessed { get; set; }
    }
}