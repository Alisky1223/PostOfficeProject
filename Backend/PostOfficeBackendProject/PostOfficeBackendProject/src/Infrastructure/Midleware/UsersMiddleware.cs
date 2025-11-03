using CommonDll.Dto;
using PostOfficeBackendProject.src.Domain.Interface;
using System.Text.Json;

namespace PostOfficeBackendProject.src.Infrastructure.Midleware
{
    public class UsersMiddleware : IUsersMiddleware
    {
        private readonly HttpClient _context;
        public UsersMiddleware(HttpClient context)
        {
            _context = context;
        }

        public async Task<ApiResponse<UserPersonalInformationDto>> GetUserInformation(int userId)
        {
            try
            {
                return await _context.GetFromJsonAsync<ApiResponse<UserPersonalInformationDto>>($"/api/users/getUserById/{userId}") ?? throw new Exception("API response is null");
            }
            catch (Exception e)
            {
                return new ApiResponse<UserPersonalInformationDto>(e.Message, 500);
            }
        }

        public async Task<ApiResponse<List<UserDto>>> GetAllUsers()
        {
            try
            {
                return await _context.GetFromJsonAsync<ApiResponse<List<UserDto>>>("/api/users/getAllUsers") ?? throw new Exception("API response is null");
            }
            catch (Exception e)
            {
                return new ApiResponse<List<UserDto>>(e.Message, 500);
            }
        }

        // Helper to reduce duplication
        private static async Task<T> HandleResponse<T>(HttpResponseMessage response, string action)
        {
            var result = await response.Content.ReadFromJsonAsync<T>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? throw new InvalidOperationException($"{action}: Deserialized response is null.");
        }
    }
}
