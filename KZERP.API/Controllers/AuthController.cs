using KZERP.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace KZERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var (ok, errors) = await _auth.RegisterAsync(dto);
            if (!ok) return BadRequest(new { Errors = errors });
            return Ok(new { Message = "Registered" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var (ok, token, error) = await _auth.LoginAsync(dto);
            if (!ok) return Unauthorized(new { Error = error });
            return Ok(new { Token = token });
        }
    }
}