//using Microsoft.AspNetCore.Identity.Data;

using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using System.Runtime.Intrinsics.X86;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860






namespace GadjetsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        UserService Userservices = new UserService();

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            using (StreamReader reader = System.IO.File.OpenText("users.txt"))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user?.userId == id)
                        return Ok(user);
                }
            }
            return NoContent();
        }


        [HttpGet]
        public ActionResult<User> Get()
        {
            List<User> users = Userservices.Get();
            return Ok(users);
        }

        [HttpPost("login")]
        public ActionResult<User> Post([FromQuery] string UserName, [FromQuery] string Password )
        {
            User userLogin = Userservices.Login(UserName,Password);
            if (userLogin != null)
            {
                return Ok( userLogin);
            }
            return NotFound("NO USER FOUND");


            //string filePath = "C:\\Users\\user\\Desktop\\תהילה\\API\\GadjetsStore\\user.txt";
            //using (StreamReader reader = System.IO.File.OpenText(filePath))
            //{
            //    string? currentUserInFile;
            //    while ((currentUserInFile = reader.ReadLine()) != null)
            //    {
            //        User? user1 = JsonSerializer.Deserialize<User>(currentUserInFile);
            //        if (user1?.Password == Password && user1?.UserName == UserName)
            //        {
            //            return user1;
            //        }
                    
            //    }
            //    return null;
            //}
            
        }


        // POST api/<UsersController>
        [HttpPost("register")]
        public ActionResult<User> Register([FromBody] User user)
        {
            User newuser = Userservices.Register(user);
            if (newuser != null)
            {
                return CreatedAtAction(nameof(Get), new { id = user.userId }, user);
            }
            else
            {
                //return StatusCode(500, "Error writing user to file ");
                return null;
            }

        }

        //[HttpPost("register")]
        //public ActionResult<User> Register([FromBody] User user)
        //{
        //    User newuser = Userservices.Register(user);
        //    if (newuser != null)
        //    {
        //        return CreatedAtAction(nameof(Get), new { id = user.userId }, user);
        //    }
        //    else
        //    {
        //        return StatusCode(500, "Registration failed. Possibly user already exists.");
        //    }

        //}

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User userToUpdate)
        {
            //string filePath = "C:\\Users\\user\\Desktop\\תהילה\\API\\GadjetsStore\\user.txt";
            //string textToReplace = string.Empty;
            //using (StreamReader reader = System.IO.File.OpenText(filePath))
            //{
            //    string? currentUserInFile;
            //    while ((currentUserInFile = reader.ReadLine()) != null)
            //    {
            //        User? user = JsonSerializer.Deserialize<User >(currentUserInFile);
            //        if (user?.userId == id)
            //            textToReplace = currentUserInFile;
            //    }
            //}
            //if (textToReplace != string.Empty)
            //{
            //    string text = System.IO.File.ReadAllText(filePath);
            //    text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
            //    System.IO.File.WriteAllText(filePath, text);
            //}
            Userservices.UpDate(userToUpdate, id);


        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
