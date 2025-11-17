using CommonDll.Dto;
using PostOfficeFrontendProject__all_interactive.Interface;
using System.Text.Json;
namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class ProductDropdownMiddelware : IProductDropdownMiddelware
    {
        private readonly HttpClient _httpClient;
        public ProductDropdownMiddelware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Type

        public async Task<ApiResponse<List<ProductTypeDto>>> GetAllProductTypeAsync()
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<List<ProductTypeDto>>>("api/productType/getAll") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<List<ProductTypeDto>>(e.Message, 500);
            }
        }

        public async Task<ApiResponse<ProductTypeDto>> CreateProductTypeAsync(ProductTypeUpdateAndCreateDto createDto)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/productType/create", createDto);

                return await HandleResponse<ApiResponse<ProductTypeDto>>(response, "Create failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<ProductTypeDto>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<ProductTypeDto>> UpdateProductTypeAsync(int id, ProductTypeUpdateAndCreateDto updateDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/productType/update/{id}", updateDto);

                return await HandleResponse<ApiResponse<ProductTypeDto>>(response, "Update failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<ProductTypeDto>(e.Message, 500);
            }

        }

        //Status

        public async Task<ApiResponse<List<TransportStatusDto>>> GetAllStatusAsync()
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<List<TransportStatusDto>>>("api/TransportStatus/getAll") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<List<TransportStatusDto>>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<TransportStatusDto>> CreateStatusAsync(TransportStatusUpdateAndCreateDto create)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/TransportStatus/create", create);

                return await HandleResponse<ApiResponse<TransportStatusDto>>(response, "Create failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<TransportStatusDto>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<TransportStatusDto>> UpdateStatusAsync(int id, TransportStatusUpdateAndCreateDto update)
        {

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/TransportStatus/update/{id}", update);

                return await HandleResponse<ApiResponse<TransportStatusDto>>(response, "Update failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<TransportStatusDto>(e.Message, 500);
            }

        }

        //handler

        private static async Task<T> HandleResponse<T>(HttpResponseMessage response, string action)
        {
            var result = await response.Content.ReadFromJsonAsync<T>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? throw new InvalidOperationException($"{action}: Deserialized response is null.");
        }

    }
}
