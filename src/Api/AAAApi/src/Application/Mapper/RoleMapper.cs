using AAA.src.Domain.Model;
using CommonDll.Dto;

namespace AAA.src.Application.Mapper
{
    public static class RoleMapper
    {
        public static Role ToRoleFromCreateDto(this RolesCreateDto createDto)
        {
            return new Role
            {
                Name = createDto.Name,
            };
        }

        public static RoleDto ToDto(this Role role)
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
    }
}
