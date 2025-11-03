using AAA.src.Application.Mapper;
using AAA.src.Domain.Interface;
using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AAA.src.Presentation.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id) 
        {
            var user = await _repository.GetUserById(id);
            if(user == null) return NotFound(new ApiResponse<object>("User not found",404));

            return Ok(new ApiResponse<object>(user.ToUserPersonalInformationDto()));
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers() 
        {
            var users = await _repository.GetUsers();
            var usersDto = users.Select(x => x.ToDto()).ToList();

            return Ok(new ApiResponse<object>(usersDto));
        }
    }
}
