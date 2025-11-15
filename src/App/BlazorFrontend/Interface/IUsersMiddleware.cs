using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IUsersMiddleware
    {
        Task<ApiResponse<List<UserDto>>> GetAllUsersAsync();
        Task<ApiResponse<UserCustomerPostmanDto>> GetByIdAsync(int id);
    }
}
