using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class TestCategoryReposetoryIntegration : IClassFixture<DBFixure>
    {
        private readonly GadjetsStoreDBContext _dbcontext;
        private readonly CategoryRepository _categoryRepository;

        public TestCategoryReposetoryIntegration(DBFixure dbFixure)
        {
            _dbcontext = dbFixure.Context;
            _categoryRepository = new CategoryRepository(_dbcontext);
        }

        [Fact]
        public async Task Get_AllCategories_ReturnsListOfCategories()
        {
            // Arrange
            var category1 = new Category
            {
                Name = "Electronics",
              
            };

            var category2 = new Category
            {
                Name = "Home Appliances",
               
            };

            await _dbcontext.Categories.AddAsync(category1);
            await _dbcontext.Categories.AddAsync(category2);

            await _dbcontext.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.Get();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count >= 2);
            Assert.Contains(result, c => c.Name == "Electronics");
            Assert.Contains(result, c => c.Name == "Home Appliances");
        }

        [Fact]
        public async Task Get_EmptyDatabase_ReturnsEmptyList()
        {
            // Act
            var result = await _categoryRepository.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        
    }
}
