using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface ILoginMiddleware
    {
        Task<ApiResponse<string>> Login(LoginDto user);
        Task<ApiResponse<UserDto>> UpdateRoleAsync(int id, int roleId);
    }
}
