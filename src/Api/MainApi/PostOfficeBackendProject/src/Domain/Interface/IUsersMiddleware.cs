using CommonDll.Dto;

namespace PostOfficeProject.Core.src.Domain.Interface
{
    public interface IUsersMiddleware
    {
        Task<ApiResponse<UserCustomerPostmanDto>> GetUserInformation(int userId);
        Task<ApiResponse<List<UserDto>>> GetAllUsers();
    }
}
