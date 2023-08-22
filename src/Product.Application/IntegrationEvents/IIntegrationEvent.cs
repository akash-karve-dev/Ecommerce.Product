namespace Product.Application.IntegrationEvents
{
    public interface IIntegrationEvent
    {
        Guid EventId { get; set; }
        string EventType { get; set; }
        DateTimeOffset CreatedAt { get; }
    }
}