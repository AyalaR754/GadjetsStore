using DTOs;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> Get();
    }
}