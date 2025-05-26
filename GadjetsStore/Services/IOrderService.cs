using DTOs;
using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task<OrderDTO> AddOrder(OrderDTO order);
        Task<List<OrderDTO>> Get();
    }
}