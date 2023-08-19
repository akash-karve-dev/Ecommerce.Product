namespace Product.Infra.Cosmos.Configurations
{
    public class CosmosDbConfiguration
    {
        public const string ConfigPath = "CosmosDb";
        public string? ConnectionString { get; set; }
        public string? DbName { get; set; }
    }
}