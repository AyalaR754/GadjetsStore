using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> Get();
        Task<User> Login(string userName, string password);
        Task<User> Register(User user);
        Task<User> UpDate(User userToUpdate, int id);
    }
}