using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class TestProductRepository
    {
        private List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Description = "Phone", Price = 1000, CategoryId = 1 },
                new Product { Id = 2, Description = "Laptop", Price = 5000, CategoryId = 2 },
                new Product { Id = 3, Description = "Phone Case", Price = 100, CategoryId = 1 },
                new Product { Id = 4, Description = "Charger", Price = 50, CategoryId = 3 }
            };
        }

        [Fact]
        public async Task Get_ReturnsAllProducts_WhenNoFilters()
        {
            // Arrange
            var products = GetSampleProducts();
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var repo = new ProductRepository(mockContext.Object);

            // Act
            var result = await repo.Get(null, null, null, new int?[] { });

            // Assert
            Assert.Equal(products.Count, result.Count);
        }

        [Fact]
        public async Task Get_FiltersByName()
        {
            // Arrange
            var products = GetSampleProducts();
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var repo = new ProductRepository(mockContext.Object);

            // Act
            var result = await repo.Get("Phone", null, null, new int?[] { });

            // Assert
            Assert.All(result, p => Assert.Contains("Phone", p.Description));
        }

        [Fact]
        public async Task Get_FiltersByMinPrice()
        {
            // Arrange
            var products = GetSampleProducts();
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var repo = new ProductRepository(mockContext.Object);

            // Act
            var result = await repo.Get(null, 1000, null, new int?[] { });

            // Assert
            Assert.All(result, p => Assert.True(p.Price >= 1000));
        }

        [Fact]
        public async Task Get_FiltersByMaxPrice()
        {
            // Arrange
            var products = GetSampleProducts();
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var repo = new ProductRepository(mockContext.Object);

            // Act
            var result = await repo.Get(null, null, 100, new int?[] { });

            // Assert
            Assert.All(result, p => Assert.True(p.Price <= 100));
        }

        [Fact]
        public async Task Get_FiltersByCategoryIds()
        {
            // Arrange
            var products = GetSampleProducts();
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var repo = new ProductRepository(mockContext.Object);

            // Act
            var result = await repo.Get(null, null, null, new int?[] { 1 });

            // Assert
            Assert.All(result, p => Assert.Equal(1, p.CategoryId));
        }

        [Fact]
        public async Task Get_FiltersByAllParameters()
        {
            // Arrange
            var products = GetSampleProducts();
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var repo = new ProductRepository(mockContext.Object);

            // Act
            var result = await repo.Get("Phone", 100, 2000, new int?[] { 1 });

            // Assert
            Assert.All(result, p =>
            {
                Assert.Contains("Phone", p.Description);
                Assert.True(p.Price >= 100 && p.Price <= 2000);
                Assert.Equal(1, p.CategoryId);
            });
        }
    }
}
