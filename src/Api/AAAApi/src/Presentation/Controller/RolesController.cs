using AAA.src.Application.Mapper;
using AAA.src.Domain.Interface;
using CommonDll.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AAA.src.Presentation.Controller
{
    [Route("api/Roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private const string getAllRolesRequest = "getAllRoles";
        private const string createRoleRequest = "addRole";

        private readonly IRoleRepository _repository;
        public RolesController(IRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpPost(createRoleRequest)]
        public async Task<IActionResult> AddRole([FromBody] RolesCreateDto createDto)
        {
            var newRole = await _repository.AddRoles(createDto.ToRoleFromCreateDto());
            return Ok(new ApiResponse<object>(newRole.ToDto()));
        }

        [HttpGet(getAllRolesRequest)]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _repository.GetRolesAsync();

            var rolesDto = roles.Select(x => x.ToDto()).ToList();

            return Ok(new ApiResponse<object>(rolesDto));
        }
    }
}
