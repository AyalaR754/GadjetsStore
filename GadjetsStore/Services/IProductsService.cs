using DTOs;

namespace Services
{
    public interface IProductsService
    {
        Task<List<ProductDTO>> Get(string? name, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}