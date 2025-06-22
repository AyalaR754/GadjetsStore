using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;



namespace Repositories 
{
    public class UserRepository : IUserRepository
    {

        GadjetsStoreDBContext _gadjetsStoreDBContext;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(GadjetsStoreDBContext gadjetsStoreDBContext, ILogger<UserRepository> logger)
        {   _logger = logger; 
            _gadjetsStoreDBContext = gadjetsStoreDBContext;
        }
        public async Task<User> Login(string userName, string password)
        {
            _logger.LogInformation("Attempting login for user: {UserName}", userName);
            return await _gadjetsStoreDBContext.Users.Where(user => user.Email.Trim() == userName && user.Password.Trim() == password).FirstOrDefaultAsync();

        }
        public async Task<User> Register(User user)
        {
            await _gadjetsStoreDBContext.Users.AddAsync(user);
            await _gadjetsStoreDBContext.SaveChangesAsync();
            return user;


        }
        public async Task<List<User>> Get()
        {
      
            List<User> users = await _gadjetsStoreDBContext.Users.ToListAsync();
            return users;
            
        }
        public async Task<User> UpDate(User userToUpdate, int id)
        {
            if (userToUpdate == null)
                return null;
            _gadjetsStoreDBContext.Users.Update(userToUpdate);
            await _gadjetsStoreDBContext.SaveChangesAsync();
            return userToUpdate;

        }
    }
}
