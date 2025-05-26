using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class TestProductReposetoryIntegration : IClassFixture<DBFixure>
    {
        private readonly GadjetsStoreDBContext _dbcontext;
        private readonly ProductRepository _productRepository;

        public TestProductReposetoryIntegration(DBFixure dbFixure)
        {
            _dbcontext = dbFixure.Context;
            _productRepository = new ProductRepository(_dbcontext);
        }

        [Fact]
        public async Task Get_AllProducts_ReturnsAllProducts()
        {
            // Arrange
            var category1 = new Category { Name = "Electronics" };
            var category2 = new Category { Name = "Home Appliances" };

            await _dbcontext.Categories.AddRangeAsync(category1, category2);
            await _dbcontext.SaveChangesAsync();

            var product1 = new Product { Name = "Laptop", Price = 1000, Description = "High-end laptop", CategoryId = category1.Id };
            var product2 = new Product { Name = "Smartphone", Price = 800, Description = "Latest smartphone", CategoryId = category2.Id };

            await _dbcontext.Products.AddRangeAsync(product1, product2);
            await _dbcontext.SaveChangesAsync();

            // Act
            var result = await _productRepository.Get(null, null, null, new int?[] { });

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count >= 2);
            Assert.Contains(result, p => p.Name == "Laptop");
            Assert.Contains(result, p => p.Name == "Smartphone");
        }

        [Fact]
        public async Task Get_FilterByName_ReturnsMatchingProducts()
        {
            // Arrange
            var category = new Category { Name = "Tablets" };
            await _dbcontext.Categories.AddAsync(category);
            await _dbcontext.SaveChangesAsync();

            var product = new Product { Name = "Tablet", Price = 500, Description = "Portable tablet", CategoryId = category.Id };
            await _dbcontext.Products.AddAsync(product);
            await _dbcontext.SaveChangesAsync();

            // Act
            var result = await _productRepository.Get("tablet", null, null, new int?[] { });

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Tablet", result.First().Name);
        }

        [Fact]
        public async Task Get_FilterByPriceRange_ReturnsMatchingProducts()
        {
            // Arrange
            var category = new Category { Name = "Accessories" };
            await _dbcontext.Categories.AddAsync(category);
            await _dbcontext.SaveChangesAsync();

            var product1 = new Product { Name = "Monitor", Price = 15100, Description = "HD Monitor", CategoryId = category.Id };
            var product2 = new Product { Name = "Keyboard", Price = 50, Description = "Mechanical Keyboard", CategoryId = category.Id };

            await _dbcontext.Products.AddRangeAsync(product1, product2);
            await _dbcontext.SaveChangesAsync();

            // Act
            var result = await _productRepository.Get(null, 15000, 15200, new int?[] { });

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Monitor", result.First().Name);
        }

        [Fact]
        public async Task Get_FilterByCategory_ReturnsMatchingProducts()
        {
            // Arrange
            var category1 = new Category { Name = "Accessories" };
            var category2 = new Category { Name = "Audio" };

            await _dbcontext.Categories.AddRangeAsync(category1, category2);
            await _dbcontext.SaveChangesAsync();

            var product1 = new Product { Name = "Mouse", Price = 30, Description = "Wireless Mouse", CategoryId = category1.Id };
            var product2 = new Product { Name = "Headphones", Price = 100, Description = "Noise-cancelling headphones", CategoryId = category2.Id };

            await _dbcontext.Products.AddRangeAsync(product1, product2);
            await _dbcontext.SaveChangesAsync();

            // Act
            var result = await _productRepository.Get(null, null, null, new int?[] { category1.Id });

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Mouse", result.First().Name);
        }

        [Fact]
        public async Task Get_NoMatchingProducts_ReturnsEmptyList()
        {
            // Act
            var result = await _productRepository.Get("NonExistentProduct", null, null, new int?[] { });

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
