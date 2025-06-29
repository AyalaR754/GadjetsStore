//using Microsoft.AspNetCore.Identity.Data;

using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using DTOs;

namespace GadjetsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<UserDTO>> Get()
        {
            List<UserDTO> users = await _userService.Get();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Post([FromBody] UserLoginDTO user)
        {
            UserDTO userLogin = await _userService.Login(user);
            if (userLogin != null)
            {
                return Ok(userLogin);
            }
            return NotFound("NO USER FOUND");
            //return userLogin != null ? Ok(userLogin) : NotFound("NO USER FOUND");
            //do also in the next function
            
        }


        // POST api/<UsersController>
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody] UserRegisterDTO user)
        {
            UserDTO newuser = await _userService.Register(user);
            if (newuser != null)
            {
                return CreatedAtAction(nameof(Get), new { id = user }, user);
            }
            else
            {
                return null;
            }

        }
        [HttpPost("checkPassword")]
        public int Post([FromBody] String pasword)
        {
           return  _userService.checkPassword(pasword);   

        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UserRegisterDTO userToUpdate)
        {
           await _userService.UpDate(userToUpdate, id);
        }
    }
}
