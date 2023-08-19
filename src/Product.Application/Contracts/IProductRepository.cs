namespace Product.Application.Contracts
{
    public interface IProductRepository
    {
        Task AddAsync(Domain.Models.Product product);
    }
}