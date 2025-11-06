using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackendProject.src.Application.Mapper;
using PostOfficeBackendProject.src.Domain.Interface;

namespace PostOfficeBackendProject.src.Presentation.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private const string getUserByUserIdRequest = "getUserById/{id:int}";
        private const string getAllUsersRequest = "getAllUsers";

        private readonly IUsersMiddleware _middleware;
        
        public UsersController(IUsersMiddleware middleware)
        {
            _middleware = middleware;
            
        }

        [HttpGet(getUserByUserIdRequest)]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var result = await _middleware.GetUserInformation(id);

            return Ok(new ApiResponse<object>(result));

        }

        [HttpGet(getAllUsersRequest)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _middleware.GetAllUsers();

            return Ok(result);
        }
    }
}
