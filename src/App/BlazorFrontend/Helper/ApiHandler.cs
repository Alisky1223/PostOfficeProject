using System.Text.Json;
namespace PostOfficeFrontendProject__all_interactive.Helper
{
    public class ApiHandler 
    {
        public static async Task<T> HandleResponse<T>(HttpResponseMessage response, string action)
        {
            var result = await response.Content.ReadFromJsonAsync<T>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? throw new InvalidOperationException($"{action}: Deserialized response is null.");
        }
    }
}
