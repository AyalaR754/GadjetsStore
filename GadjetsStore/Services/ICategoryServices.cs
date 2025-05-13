using Entities;

namespace Services
{
    public interface ICategoryServices
    {
        Task<List<Category>> Get();
    }
}