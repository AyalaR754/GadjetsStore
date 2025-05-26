using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TestUserReposetoryIntegration : IClassFixture<DBFixure>
    {

        private readonly GadjetsStoreDBContext _dbcontext;
        private readonly UserRepository _userRepository;

        public TestUserReposetoryIntegration(DBFixure dbFixure)
        {
            _dbcontext = dbFixure.Context;
            _userRepository = new UserRepository(_dbcontext);
        }

        [Fact]
        public async Task Login_ValidCredentials_returnUser()
        {
            var user = new User { Email = "Tehila@gmail.com", Password = "Password", FirstName = "Tehila", LastName = "Tehila" };
            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();

            var result = await _userRepository.Login(user.Email, user.Password);
            Assert.NotNull(result);
            
        }
        [Fact]
        public async Task Login_InvalidCredentials_ReturnsNull()
        {
            var result = await _userRepository.Login("invalid@gmail.com", "WrongPassword");
            Assert.Null(result);
        }
        [Fact]
        public async Task Register_ValidUser_ReturnsUser()
        {
            var user = new User { Email = "newuser@gmail.com", Password = "Password123", FirstName = "New", LastName = "User" };
            var result = await _userRepository.Register(user);

            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task Get_AllUsers_ReturnsListOfUsers()
        {
            var user1 = new User { Email = "user1@gmail.com", Password = "Password1", FirstName = "User1", LastName = "Test" };
            var user2 = new User { Email = "user2@gmail.com", Password = "Password2", FirstName = "User2", LastName = "Test" };

            await _dbcontext.Users.AddRangeAsync(user1, user2);
            await _dbcontext.SaveChangesAsync();

            var result = await _userRepository.Get();

            Assert.NotNull(result);
            Assert.True(result.Count >= 2);
        }

        [Fact]
        public async Task UpDate_ValidUser_ReturnsUpdatedUser()
        {
            var user = new User { Email = "updateuser@gmail.com", Password = "OldPassword", FirstName = "Old", LastName = "Name" };
            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();

            user.Password = "NewPassword";
            user.FirstName = "New";
            user.LastName = "Updated";

            var result = await _userRepository.UpDate(user, user.Id);

            Assert.NotNull(result);
            Assert.Equal("NewPassword", result.Password);
            Assert.Equal("New", result.FirstName);
            Assert.Equal("Updated", result.LastName);
        }

        [Fact]
        public async Task UpDate_NullUser_ReturnsNull()
        {
            var result = await _userRepository.UpDate(null, 0);
            Assert.Null(result);
        }
    }
}
