//using Microsoft.AspNetCore.Identity.Data;

using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Http.HttpResults;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GadjetsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userServices;

        public UsersController(IUserService userService)
        {
            _userServices = userService;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
     


        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            List<User> users = await _userServices.Get();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Post([FromQuery] string UserName, [FromQuery] string Password )
        {
            Console.WriteLine("🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶");
            User userLogin =await _userServices.Login(UserName,Password);
            if (userLogin != null)
            {
                return Ok(userLogin);
            }
            return NotFound("NO USER FOUND");
        }


        // POST api/<UsersController>
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            Console.WriteLine("🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶");
            User newuser =await  _userServices.Register(user);
            if (newuser != null)
            {
                return CreatedAtAction(nameof(Get), new { id = user }, user);
            }
            return NotFound("NO USER FOUND");
        }



        [HttpPost("checkPassword")]
        public int Post([FromBody] String pasword)
        {
           return _userServices.checkPassword(pasword);   

        }
      
        
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] User userToUpdate)
        {
         
            await _userServices.UpDate(userToUpdate, id);


        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
