using CommonDll.Dto;
using PostOfficeFrontendProject__all_interactive.Interface;
using System.Text.Json;

namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class RegisterMiddleware : IRegisterMiddleware
    {

        private readonly HttpClient _httpClient;

        public RegisterMiddleware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<string>> Register(RegisterDto userRegister)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/register", userRegister);

                return await HandleResponse<ApiResponse<string>>(response, "register failed");
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

