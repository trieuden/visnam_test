using be.DTOs;
using be.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            var token = await _userService.LoginAsync(request);

            if (token == null)
            {
                return BadRequest(new { message = "Sai tài khoản hoặc mật khẩu!" });
            }

            return Ok(new
            {
                message = "Đăng nhập thành công",
                accessToken = token
            });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var user = await _userService.GetMe();

            if (user == null) return Unauthorized();

            return Ok(user);
        }
    }
}