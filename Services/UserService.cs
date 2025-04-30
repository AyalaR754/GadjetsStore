using Entities;
using Repositories;
using System.Text.Json;

namespace Services
{
    public class UserService
    {
        UserRepository userRepository = new UserRepository();
       
        public User Login(string userName, string password)
        {

            User userfound = userRepository.Login(userName,password);
            if (userfound != null)
            {
                return userfound;
            }
            return null;
        }
        public User Register(User user)
        {
            // Check if the user already exists
            List<User> users = userRepository.Get();
            User? userfound = users.FirstOrDefault( u => u.UserName == user.UserName);
            if (userfound == null)
            {
                return userRepository.Register(user);
            }
            return null;
        }
        public List<User> Get()
        {
            List<User> users = userRepository.Get();
                return users;
        }
        public void UpDate(User user, int id)
        {

            userRepository.UpDate(user, id);
        }
    }
}

