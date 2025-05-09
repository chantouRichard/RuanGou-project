using backend.Services;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.Login(request);

            if (response.Success)
            {
                HttpContext.Session.SetString("Token", response.Data.Token);
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _authService.Register(request);

            if (response.Success)
            {
                HttpContext.Session.SetString("Token", response.Data.Token);
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("check-session")]
        public IActionResult CheckSession()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}