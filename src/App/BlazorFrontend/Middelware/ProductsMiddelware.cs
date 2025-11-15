using CommonDll.Dto;
using PostOfficeProject.Core.src.Application.Dto;
using PostOfficeFrontendProject__all_interactive.Interface;
using System.Text.Json;

namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class ProductsMiddelware : IProductsMiddelware
    {
        private readonly HttpClient _httpClient;

        public ProductsMiddelware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<ProductBasicInformationDto>>> GetAllProductsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<List<ProductBasicInformationDto>>>("api/product/getall") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<List<ProductBasicInformationDto>>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<ProductDto>> GetByIdAsync(int id)
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<ProductDto>>($"api/product/getbyId/{id}") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<ProductDto>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<ProductDto>> UpdateProductAsync(int id, ProductUpdateAndCreateDto updateDto)
        {

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/product/update/{id}", updateDto);

                return await HandleResponse<ApiResponse<ProductDto>>(response, "Update failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<ProductDto>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<ProductDto>> CreateProductAsync(ProductUpdateAndCreateDto createDto)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/product/create", createDto);

                return await HandleResponse<ApiResponse<ProductDto>>(response, "Create failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<ProductDto>(e.Message, 500);
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
