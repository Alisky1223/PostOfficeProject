using CommonDll.Dto;

namespace PostOfficeBackendProject.src.Domain.Interface
{
    public interface IUsersMiddleware
    {
        Task<ApiResponse<UserPersonalInformationDto>> GetUserInformation(int userId);
        Task<ApiResponse<List<UserDto>>> GetAllUsers();
    }
}
