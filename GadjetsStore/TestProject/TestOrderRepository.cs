using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace TestProject
{
    public class TestOrederRepository
    {
        [Fact]
        public async Task AddOrder_AddsOrderAndReturnsOrder()
        {
            // Arrange
            var order = new Order { Id = 1, UserId = 2, OrderSum = 300.0};
            var orders = new List<Order>();
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var repo = new OrderRepository(mockContext.Object);

            // Act
            var result = await repo.AddOrder(order);

            // Assert
            Assert.Equal(order, result);
            mockContext.Verify(x => x.Orders.AddAsync(order, default), Times.Once());
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task AddOrder_OrderIsAddedToDbSet()
        {
            // Arrange
            var order = new Order { Id = 2, UserId = 5, OrderSum = 100.0 };
            var orders = new List<Order>();
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var repo = new OrderRepository(mockContext.Object);

            // Act
            await repo.AddOrder(order);

            // Assert
            mockContext.Verify(x => x.Orders.AddAsync(order, default), Times.Once());
        }
    }
}