using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;
using DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GadjetsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {


        private readonly IOrderService _orderService;
           

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult<OrderDTO>> Get()
        {
         
            List<OrderDTO> orders = await _orderService.Get();
            return Ok(orders);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderDTO order)
        {

            OrderDTO newOrder = await _orderService.AddOrder(order);
            if (newOrder != null)
            {
                return CreatedAtAction(nameof(Get), new { id = order }, order);
            }
            else
            {
                return null;
            }
        }

    }
}
