using CommonDll.Dto;
using PostOfficeFrontendProject__all_interactive.Interface;
namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class ProductDropdownMiddelware : IProductDropdownMiddelware
    {
        private readonly HttpClient _httpClient;
        public ProductDropdownMiddelware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductTypeDto>> GetAllProductTypeAsync()
        {
            // Make the HTTP GET request with query parameters
            return await _httpClient.GetFromJsonAsync<List<ProductTypeDto>>("api/productType/getAll");
        }

        public async Task<ProductTypeDto> CreateProductTypeAsync(ProductTypeUpdateAndCreateDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/productType/createMeterClass", createDto);
            response.EnsureSuccessStatusCode(); // Throws if the response indicates an error
            return await response.Content.ReadFromJsonAsync<ProductTypeDto>();
        }
        public async Task<ProductTypeDto?> UpdateProductTypeAsync(int id, ProductTypeUpdateAndCreateDto updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/productType/updateMeterClass/{id}", updateDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductTypeDto>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // Handle "not found" response
            }
            else
            {
                response.EnsureSuccessStatusCode(); // Throw for other HTTP errors
                return null; // Unreachable but keeps compiler happy
            }
        }
        // Delete an existing MeterType


    }
}
