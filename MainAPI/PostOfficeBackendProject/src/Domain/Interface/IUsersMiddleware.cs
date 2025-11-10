using CommonDll.Dto;

namespace PostOfficeBackendProject.src.Domain.Interface
{
    public interface IUsersMiddleware
    {
        Task<ApiResponse<UserCustomerPostmanDto>> GetUserInformation(int userId);
        Task<ApiResponse<List<UserDto>>> GetAllUsers();
    }
}
