using Entities;

namespace Services
{
    public interface IProductsService
    {
        Task<List<Product>> Get();
    }
}