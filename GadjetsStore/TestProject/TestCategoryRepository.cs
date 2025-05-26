using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class TestCategoryRepository
    {
        [Fact]
        public async Task Get_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Gadgets" },
                new Category { Id = 2, Name = "Accessories" }
            };

            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Categories).ReturnsDbSet(categories);

            var repo = new CategoryRepository(mockContext.Object);

            // Act
            var result = await repo.Get();

            // Assert
            Assert.Equal(categories.Count, result.Count);
            Assert.Equal(categories, result);
            mockContext.Verify(x => x.Categories, Times.Once());
        }

        [Fact]
        public async Task Get_ReturnsEmptyList_WhenNoCategories()
        {
            // Arrange
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Categories).ReturnsDbSet(new List<Category>());

            var repo = new CategoryRepository(mockContext.Object);

            // Act
            var result = await repo.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            mockContext.Verify(x => x.Categories, Times.Once());
        }
    }
}
