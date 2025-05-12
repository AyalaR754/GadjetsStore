using Entities;

namespace Services
{
    public interface IUserService
    {
        int checkPassword(string password);
        Task<List<User>> Get();
        Task<User> Login(string userName, string password);
        Task<User> Register(User user);
        Task<User> UpDate(User user, int id);
    }
}