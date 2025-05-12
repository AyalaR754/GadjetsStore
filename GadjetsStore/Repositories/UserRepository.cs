using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;



namespace Repositories
{
    public class UserRepository : IUserRepository
    {


        GadjetsStoreContext _gadjetsStoreContext;
        public UserRepository(GadjetsStoreContext gadjetsStoreContext)
        {
            _gadjetsStoreContext = gadjetsStoreContext;
        }
        public async Task<User> Login(string userName, string password)
        {

            return await _gadjetsStoreContext.Users.Where(user => user.Email.Trim() == userName && user.Password.Trim() == password).FirstOrDefaultAsync();
            
        }
        public async Task<User> Register(User user)
        {
   
            await _gadjetsStoreContext.Users.AddAsync(user);
            await _gadjetsStoreContext.SaveChangesAsync();
            return user;
        }


        public async Task<List<User>> Get()
        {
           List<User> users= await _gadjetsStoreContext.Users.ToListAsync();
           
            return users;
        }
        public async Task<User> UpDate(User userToUpdate, int id)
        {
            if(userToUpdate!=null)
            { 
            //await _gadjetsStoreContext.Users.Where(u => u.Email == userToUpdate.Email).FirstOrDefaultAsync();
            _gadjetsStoreContext.Users.Update(userToUpdate);
            await _gadjetsStoreContext.SaveChangesAsync();
            return userToUpdate;}
            return null;
        }
    }
}
