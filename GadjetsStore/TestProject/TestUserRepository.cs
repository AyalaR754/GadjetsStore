using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;

namespace TestProject
{
    public class TestUserRepository
    {
        [Fact]
        public async Task GetUser_ReturnSameUser()
        {
            //1.Arrange
            User user = new User { Id = 1, FirstName = "ayali", LastName = "rachelzon", Password = "123", Email = "ayali@gmail.com" };
            //User user2 = new User { Id =2, FirstName = "tehila", LastName = "gil", Password = "456", Email = "tehila@gmail.com" };

            var mockContext = new Mock<GadjetsStoreDBContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);


            //Act
            var result = await userRepository.Login(user.Email, user.Password);

            Assert.Equal(result, user);


        }
        [Fact]
        public async Task Get_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "ayali", LastName = "rachelzon", Password = "123", Email = "ayali@gmail.com" },
                new User { Id = 2, FirstName = "tehila", LastName = "gil", Password = "456", Email = "tehila@gmail.com" }
            };
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var repo = new UserRepository(mockContext.Object);

            // Act
            var result = await repo.Get();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.Email == "ayali@gmail.com");
            Assert.Contains(result, u => u.Email == "tehila@gmail.com");
        }
        [Fact]
        public async Task Update_UpdatesUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Id = 1, FirstName = "ayali", LastName = "rachelzon", Password = "123", Email = "ayali@gmail.com" };
            var users = new List<User> { user };
            var mockContext = new Mock<GadjetsStoreDBContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var repo = new UserRepository(mockContext.Object);

            var updatedUser = new User { Id = 1, FirstName = "Ayala", LastName = "Rachelzon", Password = "789", Email = "ayali@gmail.com" };

            // Act
            var result = await repo.UpDate(updatedUser, 1);

            // Assert
            Assert.Equal(updatedUser, result);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task Update_ReturnsNull_WhenUserIsNull()
        {
            // Arrange
            var mockContext = new Mock<GadjetsStoreDBContext>();
            var repo = new UserRepository(mockContext.Object);

            // Act
            var result = await repo.UpDate(null, 1);

            // Assert
            Assert.Null(result);
        }
    }
}
