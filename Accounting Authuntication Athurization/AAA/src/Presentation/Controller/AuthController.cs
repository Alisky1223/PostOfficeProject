using AAA.src.Domain.Interface;
using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AAA.src.Presentation.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            var userToken = await _repository.LoginAsync(model, ipAddress);

            if (userToken == null) return Unauthorized(new ApiResponse<object>("Invalid credentials", 400));

            return Ok(new ApiResponse<object>(userToken));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var registerResult = await _repository.RegisterAsync(model);

            if (registerResult == null) return Unauthorized(new ApiResponse<object>("Username already exists", 400));

            return Ok(new ApiResponse<object>(registerResult));
        }
    }
}
