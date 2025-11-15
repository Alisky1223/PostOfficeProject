using CommonDll.Dto;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackendProject.src.Domain.Interface;

namespace PostOfficeBackendProject.src.Presentation.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string loginRequest = "login";
        private const string registerRequest = "register";
        private const string changeUserRoleRequest = "changeUserRole/{id:int}";
        private const string getallrolesRequest = "getallRoles";

        private readonly IAuthenticationMiddleware _middleware;
        public AuthController(IAuthenticationMiddleware middleware)
        {
            _middleware = middleware;
        }

        [HttpPost(loginRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>("Invalid request data", 400));

            var result = await _middleware.Login(model);

            return Ok(result);
        }

        [HttpPost(registerRequest)]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)), 400));

            var result = await _middleware.Register(model);

            return Ok(result);
        }

        [HttpPut(changeUserRoleRequest)]
        //[Authorize(Policy = "AdminOrSuperAdminPolicy")]
        public async Task<IActionResult> ChangeUserRole([FromRoute] int id, [FromBody] int newRoleId)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)), 400));

            var result = await _middleware.ChangeUserRole(id,newRoleId);

            return Ok(result);
        }

        [HttpGet(getallrolesRequest)]
        public async Task<IActionResult> GetAllRoles() 
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)), 400));

            var result = await _middleware.GetallRoles();

            return Ok(result);
        }
    }
}
