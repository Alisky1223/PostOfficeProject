using CommonDll.Dto;
using PostOfficeProject.Core.src.Domain.Interface;
using System.Text.Json;

namespace PostOfficeProject.Core.src.Infrastructure.Midleware
{
    public class AuthenticationMiddleware : IAuthenticationMiddleware
    {
        private readonly HttpClient _httpClient;

        public AuthenticationMiddleware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<string>> Login(LoginDto loginDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginDto);

                return await HandleResponse<ApiResponse<string>>(response, "Login failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<string>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<string>> Register(RegisterDto registerDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/register", registerDto);

                return await HandleResponse<ApiResponse<string>>(response, "Registration failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<string>(e.Message, 500);
            }
        }

        public async Task<ApiResponse<UserDto>> ChangeUserRole(int id, int newRoleId)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/auth/changeUserRole/{id}", newRoleId);

                return await HandleResponse<ApiResponse<UserDto>>(response, "Change role failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<UserDto>(e.Message, 500);
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