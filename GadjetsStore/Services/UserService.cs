using Entities;
using Repositories;
using Zxcvbn;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using DTOs;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Login(UserLoginDTO userLoginDTO)
        {
            if (checkPassword(userLoginDTO.Password) <= 2)
                return null;


            User userfound = await _userRepository.Login(userLoginDTO.Email, userLoginDTO.Password);
            if (userfound != null)
            {

                return _mapper.Map<User, UserDTO>(userfound);
            }
            return null;
        }
        public async Task<UserDTO?> Register(UserRegisterDTO user)
        {
            //check the password
            if (checkPassword(user.Password) <= 2)
                return null;

            // Check if the user already exists
            List<User> users = await _userRepository.Get();
            //User? userfound = users.FirstOrDefault(u => u.Email == user.Email.Trim());

            User? userfound = users.Where(u => u.Email == user.Email).FirstOrDefault();
            if (userfound == null)
            {
                User userToRegister = _mapper.Map<UserRegisterDTO, User>(user);
                User userRegisterd = await _userRepository.Register(userToRegister);
                return (userRegisterd != null) ? _mapper.Map<User, UserDTO>(userRegisterd) : null;
            }
            return null;
        }
        public async Task<List<UserDTO>> Get()
        {
            List<User> users = await _userRepository.Get();
            List<UserDTO> usersDTOs = _mapper.Map<List<User>, List<UserDTO>>(users);

            return usersDTOs;
        }
        public async Task<UserDTO> UpDate(UserRegisterDTO user, int id)
        {
            if (checkPassword(user.Password) <= 2)
                return null;

            // Check if the user already exists
            //List<User> users =await _userRepository.Get();
            //User? userfound = users.FirstOrDefault(u => u.Email == user.Email);
            //if (userfound != null)
            //{
            User userToUpdate = _mapper.Map<UserRegisterDTO, User>(user);
            userToUpdate.Id = id;
            User userUpdated = await _userRepository.UpDate(userToUpdate, id);
            if (userUpdated != null)
            {
                userUpdated.Id = id; 
                return _mapper.Map<User, UserDTO>(userUpdated);
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

