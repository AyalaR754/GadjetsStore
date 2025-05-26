using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> Get(string? name, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}