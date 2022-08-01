using AKAPI.Models;
using AKAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AKAPI.Controllers
{
    [Authorize]
    [Route("api/Users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] User user)
        {
            var u = _userRepository.Authenticate(user.Name, user.Password);
            
            if (u == null)
            {
                return BadRequest(new { message = "Username or Password doesn't match." });
            }

            return Ok(u);
        }

        [AllowAnonymous]
        [HttpPost("Regitster")]
        public IActionResult Register([FromBody] User user)
        {
            var isUnique = _userRepository.IsUniqueUser(user.Name);

            if (!isUnique)
            {
                return BadRequest(new { message = "UserId already exists." });
            }

            var u = _userRepository.Register(user.Name, user.Password);

            if(u == null)
            {
                return BadRequest(new { message = "Error whilst registering" });
            }

            return Ok();
        }
    }
}
