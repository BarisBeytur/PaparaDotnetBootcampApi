using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaDotnetBootcampApi.Business.Services.Abstract;
using PaparaDotnetBootcampApi.Entities.Entities;

namespace PaparaDotnetBootcampApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {

        private readonly IUserService _userService;

        public AuthsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = _userService.Authenticate(loginUser.Username, loginUser.Password);
            if (user == null)
                return Unauthorized();

            return Ok("Successfully logged in.");
        }

    }
}
