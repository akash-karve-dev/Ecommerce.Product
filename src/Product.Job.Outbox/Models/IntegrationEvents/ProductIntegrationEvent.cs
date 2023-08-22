using Product.Application.IntegrationEvents;
using System;

namespace Product.Job.Outbox.Models.IntegrationEvents
{
    /// <summary>
    ///  Product Outgoing/Integration event
    /// </summary>
    internal class ProductIntegrationEvent : IIntegrationEvent
    {
        public Guid EventId { get; set; }
        public string EventType { get; set; }
        public DateTimeOffset CreatedAt => DateTimeOffset.UtcNow;
        public string EventPayload { get; set; }
    }
}