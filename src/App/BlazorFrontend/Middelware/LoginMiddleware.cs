using PostOfficeFrontendProject__all_interactive.Helper;
using PostOfficeFrontendProject__all_interactive.Interface;

namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class LoginMiddleware : ILoginMiddleware
    {

        private readonly HttpClient _httpClient;

        public LoginMiddleware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<string>> Login(LoginDto user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/login", user);

                return await ApiHandler.HandleResponse<ApiResponse<string>>(response, "Login failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<string>(e.Message, 500);
            }
        }

        public async Task<ApiResponse<UserDto>> UpdateRoleAsync(int id, int roleId)
        {

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/auth/changeUserRole/{id}", roleId);

                return await ApiHandler.HandleResponse<ApiResponse<UserDto>>(response, "Update failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<UserDto>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<List<RoleDto>>> GetAllRoleAsync()
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<List<RoleDto>>>("api/auth/getallRoles") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<List<RoleDto>>(e.Message, 500);
            }

        }

    }

}


