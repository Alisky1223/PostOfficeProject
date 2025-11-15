using CommonDll.Dto;
using PostOfficeFrontendProject__all_interactive.Interface;
using PostOfficeFrontendProject__all_interactive.Helper; 
namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class UsersMiddleware : IUsersMiddleware
    {

        private readonly HttpClient _httpClient;

        public UsersMiddleware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<UserDto>>> GetAllUsersAsync()
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<List<UserDto>>>("api/users/getAllUsers") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<List<UserDto>>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<UserCustomerPostmanDto>> GetByIdAsync(int id)
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<UserCustomerPostmanDto>>($"api/users/getUserById/{id}") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<UserCustomerPostmanDto>(e.Message, 500);
            }

        }

    }
}
