using Entities;
using Repositories;
using Zxcvbn;
using System.Text.Json;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Login(string userName, string password)
        {
            if (checkPassword(password) <= 2)
                return null;


            User userfound =await _userRepository.Login(userName, password);
            if (userfound != null)
            {
                return userfound;
            }
            return null;
        }
        public async Task<User> Register(User user)
        {
            //check the password
            if (checkPassword(user.Password) <= 2)
                return null;

            // Check if the user already exists
            List<User> users = await _userRepository.Get();
            //User? userfound = users.FirstOrDefault(u => u.Email == user.Email.Trim());
            User? userfound =users.Where(u => u.Email == user.Email).FirstOrDefault();
            if (userfound == null)
            {
                return await _userRepository.Register(user);
            }
            return null;
        }
        public async Task<List<User>> Get()
        {
            List<User> users = await  _userRepository.Get();
            return users;
        }
        public async Task<User> UpDate(User user, int id)
        {
            if (checkPassword(user.Password) <= 2)
                return null;

            // Check if the user already exists
            //List<User> users =await _userRepository.Get();
            //User? userfound = users.FirstOrDefault(u => u.Email == user.Email);
            //if (userfound != null)
            //{
                return await _userRepository.UpDate(user, id);
            //}
            return null;



        }



        public int checkPassword(String password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;

        }
    }

}

