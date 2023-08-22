using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebChatApp.Data;
using WebChatApp.DTO;
using WebChatApp.Interfaces;
using WebChatApp.Models;
using WebChatApp.Repository;

namespace WebChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserController(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> GetUser([FromQuery] string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null) return BadRequest(ModelState);

            UserDTO userCreate = new UserDTO()
            {
                UserName = user.UserName,
            };

            return Ok(userCreate);
        }

        [HttpGet("Name/{userName}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        public async Task<IActionResult> GetUserByName([FromQuery] string userName)
        {
            User user = await _userManager.FindByNameAsync(userName);

            if (user == null) return BadRequest(ModelState);

            UserDTO userCreate = new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName
            };

            return Ok(userCreate);
        }

        [HttpPost("Registr")]
        public async Task<IActionResult> Registr([FromBody] UserCreate user)
        {
            if (user == null || user.UserName == "" || user.Password == "")
                return BadRequest("Not all data is filled in");

            var userCheck = await _userManager.FindByNameAsync(user.UserName);

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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserCreate user)
        {
            if (user == null || user.UserName == "" || user.Password == "")
                return BadRequest("Not all data is filled in");

            var userCheck = await _userManager.FindByNameAsync(user.UserName);

            if (userCheck == null)
            {
                return BadRequest("This user does not exist");
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(userCheck, user.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(userCheck, user.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok(userCheck.Id);
                }
            }

            return BadRequest("Wrong credentials. Please, try again");
        }


        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUser([FromQuery] string userName)
        {
            if (await _userRepository.DeleteUser(userName) != 1)
            {
                return BadRequest("Пользователь не удалён");
            }

            if (await _userRepository.DeleteEmptyChats() != 1)
            {
                return BadRequest("Пользователь удалён, но пустые чаты не очистились");
            }
            return Ok("Пользователь со всеми данными успешно удалён");

        }

        //[HttpPost("TokenLogin")]
        //public async Task<IActionResult> TokenLogin([FromBody] UserCreate user)
        //{
        //    if (user == null || user.UserName == "" || user.Password == "")
        //        return BadRequest("Not all data is filled in");

        //    var userCheck = await _userManager.FindByNameAsync(user.UserName);

        //    if (userCheck == null)
        //    {
        //        return BadRequest("This user does not exist");
        //    }

        //    var passwordCheck = await _userManager.CheckPasswordAsync(userCheck, user.Password);
        //    if (passwordCheck)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(userCheck, user.Password, false, false);
        //        if (result.Succeeded)
        //        {
        //            //var token = GenerateToken();

        //            return Ok(userCheck.Id);
        //        }
        //    }

        //    return BadRequest("Wrong credentials. Please, try again");
        //}

        //private string GenerateToken()
        //{

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        expires: DateTime.UtcNow.AddHours(1), // Время жизни токена
        //        signingCredentials: credentials
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
