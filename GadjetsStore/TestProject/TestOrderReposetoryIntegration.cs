using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using Xunit;
 
namespace TestProject
{
    public class TestOrderReposetoryIntegration : IClassFixture<DBFixure>
    {
        private readonly GadjetsStoreDBContext _dbcontext;
        private readonly OrderRepository _orderRepository;

        public TestOrderReposetoryIntegration(DBFixure dbFixure)
        {
            _dbcontext = dbFixure.Context;
            _orderRepository = new OrderRepository(_dbcontext);
        }

        [Fact]
        public async Task AddOrder_ShouldAddOrderToDatabase()
        {

            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Password = "password123"
            };

            // Add the user to the database
            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();

            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderSum = 150.75,
                UserId = user.Id
            };

            // Act
            await _orderRepository.AddOrder(order);

            // Assert
            var savedOrder = await _dbcontext.Orders.FindAsync(order.Id);
            Assert.NotNull(savedOrder);
            Assert.Equal(order.OrderDate.Date, savedOrder.OrderDate.Date);
            Assert.Equal(order.OrderSum, savedOrder.OrderSum);
            Assert.Equal(order.UserId, savedOrder.UserId);
        }

        [Fact]
        public async Task AddOrder_WithInvalidUserId_ShouldNotAddOrder()
        {
            // Arrange
            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderSum = 200.50,
                UserId = -1
            };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () =>
            {
                await _orderRepository.AddOrder(order);
            });
        }
    }
}
