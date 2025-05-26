using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;
using DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GadjetsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<CategoryDTO>> Get()
        {
            List<CategoryDTO> categories = await _categoryService.Get();
            return Ok(categories);
        }

    }
}
