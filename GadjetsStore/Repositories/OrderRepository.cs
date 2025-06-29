using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        GadjetsStoreDBContext _gadjetsStoreDBContext;

        public OrderRepository(GadjetsStoreDBContext gadjetsStoreDBContext)
        {
            _gadjetsStoreDBContext = gadjetsStoreDBContext;
        }
        public async Task<List<Order>> Get()
        {
            return await _gadjetsStoreDBContext.Orders.ToListAsync();//change in all the functions
        }

        public async Task<Order> AddOrder(Order order)
        {

            await _gadjetsStoreDBContext.Orders.AddAsync(order);
            await _gadjetsStoreDBContext.SaveChangesAsync();
            return order;
        }

    }
}
