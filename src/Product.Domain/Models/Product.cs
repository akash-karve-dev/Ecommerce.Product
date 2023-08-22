using Product.Domain.Enums;

namespace Product.Domain.Models
{
    public class Product : DomainEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public ProductCategory? Category { get; set; }
    }
}