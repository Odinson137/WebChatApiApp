using Microsoft.AspNetCore.Mvc;
using WebChatApp.Data;
using WebChatApp.DTO;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            ICollection<UserDTO> users = _userRepository.GetUsers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }


        [HttpGet("{userID}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        public IActionResult GetUser(int userID)
        {
            User user = _userRepository.GetUser(userID);

            if (user == null) return BadRequest(ModelState);

            UserDTO userCreate = new UserDTO()
            {
                Name = user.Name,
                LastName = user.LastName
            };

            return Ok(userCreate);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserCreate user)
        {
            if (user == null)
                return BadRequest(ModelState);

            var userBD = _userRepository.GetUser(user.Name);

            if (userBD != null)
            {
                ModelState.AddModelError("", "Такой пользователь уже существует");
                return StatusCode(422, ModelState);
            }
            User user1 = new User()
            {
                Name = user.Name,
                LastName = user.LastName,
                Password = user.Password
            };

            _userRepository.CreateUser(user1);
         
            return Ok("Succecfully created");
        }
    }
}
