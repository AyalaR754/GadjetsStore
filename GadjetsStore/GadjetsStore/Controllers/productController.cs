using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;
using DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GadjetsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdactController : ControllerBase
    {

        private readonly IProductsService _productsService;
        public ProdactController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> Get([FromQuery]  string? name, [FromQuery] int? minPrice, [FromQuery]  int? maxPrice, [FromQuery]  int?[] categoryIds)
        {
            List<ProductDTO> categories = await _productsService.Get( name,  minPrice, maxPrice,categoryIds);
            return Ok(categories);
        }
  
    }
}
