using CommonDll.Dto;
using Microsoft.AspNetCore.Authentication;
using PostOfficeBackendProject.src.Application.Dto;
using PostOfficeFrontendProject__all_interactive.Interface;
using System.Net.Http.Json;
using System.Text.Json;

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

                return await HandleResponse<ApiResponse<string>>(response, "Login failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<string>(e.Message, 500);
            }
        }

        private static async Task<T> HandleResponse<T>(HttpResponseMessage response, string action)
        {
            var result = await response.Content.ReadFromJsonAsync<T>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? throw new InvalidOperationException($"{action}: Deserialized response is null.");
        }
    }
}


