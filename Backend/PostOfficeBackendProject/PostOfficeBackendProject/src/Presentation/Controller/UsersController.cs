using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackendProject.src.Domain.Interface;

namespace PostOfficeBackendProject.src.Presentation.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersMiddleware _middleware;
        public UsersController(IUsersMiddleware middleware)
        {
            _middleware = middleware;
        }

        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var result = await _middleware.GetUserInformation(id);

            if (!result.Success) return BadRequest(new ApiResponse<object>(result.Message, 500));

            switch (result.Data.Role.Name)
            {
                case "SuperAdmin":
                    break;
                case "Admin":
                    break;
                case "Customer":
                    break;
                case "Postman":
                    break;
                case "User":
                    break;
            }

            return Ok(result);
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _middleware.GetAllUsers();

            return Ok(result);
        }
    }
}
