using Entities;

namespace Services
{
    public interface IProductServices
    {
        Task<List<Product>> Get();
    }
}