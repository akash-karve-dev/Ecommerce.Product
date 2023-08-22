namespace Product.Infra.AzureServiceBus.Configurations
{
    internal class AzureServiceBusConfiguration
    {
        public const string ConfigPath = "AzureServiceBus";

        public string? ConnectionString { get; set; }

        ///// <summary>
        /////  Topic/Queue name
        ///// </summary>
        //public string? TopicName { get; set; }
    }
}