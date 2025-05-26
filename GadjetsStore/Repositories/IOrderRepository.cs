using Entities;
namespace Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> Get();
        Task<Order> AddOrder(Order order);
    }
}