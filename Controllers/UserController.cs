using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public UserController(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUsers()
        {
            ICollection<UserDTO> users = await _userRepository.GetUsers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        public async Task<IActionResult> GetUser(string userID)
        {
            User user = await _userRepository.GetUser(id: userID);

            if (user == null) return BadRequest(ModelState);

            UserDTO userCreate = new UserDTO()
            {
                UserName = user.UserName,
            };

            return Ok(userCreate);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreate user)
        {
            if (user == null || user.UserName == "" || user.Password == "")
                return BadRequest("Not all data is filled in");

            var userCheck = await _userRepository.GetUser(userName: user.UserName);

            if (userCheck != null)
            {
                return BadRequest("This username is already taken");
            }

            User createUser = new User()
            {
                UserName = user.UserName,
            };

            var result = await _userManager.CreateAsync(createUser, user.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors.First().Description);

            //await _userManager.AddToRoleAsync(createUser, UserRoles.User);
            return Ok(createUser.Id);
        }
    }
}
