using backend.Application.Services;
using backend.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Hardcoded username/password for demonstration
            if (loginRequest.Username == "admin" && loginRequest.Password == "password123")
            {
                var token = _jwtService.GenerateToken(loginRequest.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password");
        }
    }
}
