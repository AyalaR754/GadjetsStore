using DTOs;

namespace Services
{
    public interface IUserService
    {
        int checkPassword(string password);
        Task<List<UserDTO>> Get();
        Task<UserDTO> Login(UserLoginDTO userLoginDTO);
        Task<UserDTO?> Register(UserRegisterDTO user);
        Task<UserDTO> UpDate(UserRegisterDTO user, int id);
    }
}