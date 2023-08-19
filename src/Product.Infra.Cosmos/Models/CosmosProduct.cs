namespace Product.Infra.Cosmos.Models
{
    public class CosmosProduct : Domain.Models.Product
    {
        public string? PartitionKey { get; set; }
        public string? Discriminator { get; set; } = "ProductEntity";
    }
}