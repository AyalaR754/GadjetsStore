using System.Threading.Tasks;
using AutoMapper;
using DTOs;
using Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Repositories;
using Services;
using Xunit;

public class UserServiceTests
{
    // --------- טסטים לחוזק סיסמה ---------

    [Fact]
    public void CheckPassword_ReturnsZero_ForVeryWeakPassword()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<UserService>>();
        var service = new UserService(mockRepo.Object, mockMapper.Object, mockLogger.Object);
        string password = "123";

        // Act
        int score = service.checkPassword(password);

        // Assert
        Assert.Equal(0, score);
    }

    [Fact]
    public void CheckPassword_ReturnsZero_ForCommonPassword()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<UserService>>();
        var service = new UserService(mockRepo.Object, mockMapper.Object, mockLogger.Object);
        string password = "password";

        // Act
        int score = service.checkPassword(password);

        // Assert
        Assert.Equal(0, score);
    }

    [Fact]
    public void CheckPassword_ReturnsTwo_ForMediumPassword()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<UserService>>();
        var service = new UserService(mockRepo.Object, mockMapper.Object, mockLogger.Object);
        string password = "abc123";

        // Act
        int score = service.checkPassword(password);

        // Assert
        Assert.Equal(2, score);
    }

    [Fact]
    public void CheckPassword_ReturnsFour_ForStrongPassword()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<UserService>>();
        var service = new UserService(mockRepo.Object, mockMapper.Object, mockLogger.Object);
        string password = "StrongPass123!";

        // Act
        int score = service.checkPassword(password);

        // Assert
        Assert.Equal(4, score);
    }

    [Fact]
    public async Task Login_ReturnsNull_WhenPasswordIsWeak()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<UserService>>();
        var service = new UserService(mockRepo.Object, mockMapper.Object, mockLogger.Object);

        var loginDto = new UserLoginDTO ( "user@example.com", "123" );

        // Act
        var result = await service.Login(loginDto);

        // Assert
        Assert.Null(result);
    }

    // --------- טסטים ללוגר ---------

    [Fact]
    public async Task Login_LogsWarning_WhenUserNotFound()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<UserService>>();
        var service = new UserService(mockRepo.Object, mockMapper.Object, mockLogger.Object);

        var loginDto = new UserLoginDTO ("user@example.com", "StrongPass123!");
        mockRepo.Setup(r => r.Login(loginDto.Email, loginDto.Password)).ReturnsAsync((User)null);

        // Act
        var result = await service.Login(loginDto);

        // Assert
        Assert.Null(result);
        mockLogger.Verify(l => l.Log(
            LogLevel.Warning,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Login failed")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Login_LogsInformation_WhenUserFound()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<UserService>>();
        var service = new UserService(mockRepo.Object, mockMapper.Object, mockLogger.Object);

        var user = new User { Id = 1, Email = "test@example.com", Password = "StrongPass123!" };
        var userDto = new UserDTO ( 1, "ayali","tehila","test@example.com" );
        var loginDto = new UserLoginDTO ("user@example.com", "StrongPass123!");

        mockRepo.Setup(r => r.Login(loginDto.Email, loginDto.Password)).ReturnsAsync(user);
        mockMapper.Setup(m => m.Map<User, UserDTO>(user)).Returns(userDto);

        // Act
        var result = await service.Login(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userDto.Email, result.Email);

        mockLogger.Verify(l => l.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("logged in successfully")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }
}
