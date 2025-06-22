using Xunit;
using Moq;
using Services;
using Repositories;
using DTOs;
using Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject
{
    public class TestUserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
       
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<UserService>>();

          
        }

        [Fact]
        public async Task Register_With123Password_ShouldReturnNull()
        {
            // Arrange
            var user = new UserRegisterDTO { Email = "weak1@test.com", Password = "123" };

            // Act
            var result = await _userService.Register(user);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Register_WithPassword1_ShouldReturnNull()
        {
            // Arrange
            var user = new UserRegisterDTO { Email = "weak2@test.com", Password = "password1" };

            // Act
            var result = await _userService.Register(user);

            // Assert
            Assert.Null(result);
        }
        public async Task Register_StrongPassword_UserDoesNotExist_ReturnsUserDTO()
        {
            // Arrange
            var dto = new UserRegisterDTO { Email = "strong@test.com", Password = "Very$trongPass123" };
            var entity = new User { Id = 1, Email = dto.Email };
            var expectedDto = new UserDTO { Id = 1, Email = dto.Email };

            _userRepositoryMock.Setup(r => r.Get()).ReturnsAsync(new List<User>());
            _userRepositoryMock.Setup(r => r.Register(It.IsAny<User>())).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<UserRegisterDTO, User>(dto)).Returns(entity);
            _mapperMock.Setup(m => m.Map<User, UserDTO>(entity)).Returns(expectedDto);

            // Act
            var result = await _userService.Register(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Email, result.Email);
        }

        [Fact]
        public void CheckPassword_StrengthScore_ReturnsCorrectScore()
        {
            // Arrange
            var weakPassword = "123";
            var strongPassword = "My$ecurePass123";

            // Act
            var weakScore = _userService.checkPassword(weakPassword);
            var strongScore = _userService.checkPassword(strongPassword);

            // Assert
            Assert.True(weakScore <= 2);
            Assert.True(strongScore >= 3);
        }
    }
}


