using Entities;
using Repositories;
using Zxcvbn;
using System.Text.Json;

namespace Services
{
    public class UserService
    {
        UserRepository userRepository = new UserRepository();
       
        public User Login(string userName, string password)
        {
            if (checkPassword(password) <= 2)
                return null;

         
            User userfound = userRepository.Login(userName,password);
            if (userfound != null)
            {
                return userfound;
            }
            return null;
        }
        public User Register(User user)
        {
            //check the password
            if (checkPassword(user.Password)<= 2)
                return null;

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
        public User UpDate(User user, int id)
        {
            if (checkPassword(user.Password) <= 2)
                return null;

            // Check if the user already exists
            List<User> users = userRepository.Get();
            User? userfound = users.FirstOrDefault(u => u.UserName == user.UserName);
            if (userfound == null)
            {
                return userRepository.UpDate(user, id);
            }
            return null;
            
        }
    


    public int checkPassword(String password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;

        }
    }

}

