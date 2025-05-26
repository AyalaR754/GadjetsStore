using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record UserDTO(int Id, string FirstName, string LastName, string Email);
    public record UserRegisterDTO(string FirstName, string LastName, string Email, string Password);

    public record UserLoginDTO(string Email, string Password);
}
