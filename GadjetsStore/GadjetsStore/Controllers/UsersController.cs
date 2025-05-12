//using Microsoft.AspNetCore.Identity.Data;

using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860






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


        // GET api/<UsersController>/5
        //[HttpGet("{id}")]
        //public ActionResult<User> Get(int id)
        //{
        //    using (StreamReader reader = System.IO.File.OpenText("users.txt"))
        //    {
        //        string? currentUserInFile;
        //        while ((currentUserInFile = reader.ReadLine()) != null)
        //        {
        //            User? user = JsonSerializer.Deserialize<User>(currentUserInFile);
        //            if (user?.userId == id)
        //                return Ok(user);
        //        }
        //    }
        //    return NoContent();
        //}


        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            List<User> users = await _userService.Get();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Post([FromQuery] string UserName, [FromQuery] string Password )
        {
            User userLogin = await _userService.Login(UserName,Password);
            if (userLogin != null)
            {
                return Ok( userLogin);
            }
            return NotFound("NO USER FOUND");
            
        }


        // POST api/<UsersController>
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            User newuser = await _userService.Register(user);
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
        public async Task Put(int id, [FromBody] User userToUpdate)
        {
           await _userService.UpDate(userToUpdate, id);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
