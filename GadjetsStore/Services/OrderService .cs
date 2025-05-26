using AutoMapper;
using DTOs;
using Entities;

using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<List<OrderDTO>> Get()
        {
            List<Order> orders = await _orderRepository.Get();
            return _mapper.Map<List<Order>, List<OrderDTO>>(orders); ;
        }

        public async Task<OrderDTO> AddOrder(OrderDTO order)
        {
            Order orderToAdd=_mapper.Map<OrderDTO, Order>(order);
            Order order1 = await _orderRepository.AddOrder(orderToAdd);
            return _mapper.Map<Order, OrderDTO>(order1);
        }


    }
}
