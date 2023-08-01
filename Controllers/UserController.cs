using Microsoft.AspNetCore.Mvc;
using WebChatApp.Interfaces;
using WebChatApp.Models;

namespace WebChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }


        [HttpGet("{userID}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetUser(int userID)
        {
            User user = _userRepository.GetUser(userID);
            if (user == null) return BadRequest(ModelState);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult NewUser()
        {
            User user1 = new User()
            {
                Name = "Sasha",
                LastName = "Baget",
                Password = "246532w4643@!"
            };

            _userRepository.NewUser(user1);
         
            return Ok(user1);
        }
    }
}
