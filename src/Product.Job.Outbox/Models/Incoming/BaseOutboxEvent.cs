namespace Product.Job.Outbox.Models.Incoming
{
    internal class BaseOutboxEvent
    {
        public string Discriminator { get; set; }
    }
}