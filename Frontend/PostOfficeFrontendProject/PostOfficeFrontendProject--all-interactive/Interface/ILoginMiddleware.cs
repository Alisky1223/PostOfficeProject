using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface ILoginMiddleware
    {
        Task<ApiResponse<string>> Login(LoginDto user);
    }
}
