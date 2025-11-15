using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IRegisterMiddleware
    {
        Task<ApiResponse<string>> Register(RegisterDto userRegister);
    }
}
