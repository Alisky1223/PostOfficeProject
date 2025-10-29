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
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>("Invalid request data", 400));

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var result = await _repository.LoginAsync(model, ipAddress);

            return result.StatusCode switch
            {
                200 => Ok(new ApiResponse<object>(result.Token!)),
                401 => Unauthorized(new ApiResponse<object>(result.Message!, 401)),
                403 => StatusCode(403, new ApiResponse<object>(result.Message!, 403)),
                _ => StatusCode(500, new ApiResponse<object>("An unexpected error occurred.", 500))
            };
        }

        [HttpPost("register")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)), 400));

            var registerResult = await _repository.RegisterAsync(model);

            if (registerResult == null) return Unauthorized(new ApiResponse<object>("Username already exists", 400));

            return Ok(new ApiResponse<object>(registerResult));
        }
    }
}
