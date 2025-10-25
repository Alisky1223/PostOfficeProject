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

        //Type

        public async Task<List<ProductTypeDto>> GetAllProductTypeAsync()
        {
           
            return await _httpClient.GetFromJsonAsync<List<ProductTypeDto>>("api/productType/getAll");
        }

        public async Task<ProductTypeDto> CreateProductTypeAsync(ProductTypeUpdateAndCreateDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/productType/createMeterClass", createDto);
            response.EnsureSuccessStatusCode();
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
                return null;
            }
            else
            {
                response.EnsureSuccessStatusCode();
                return null;
            }
        }

        //Status

        public async Task<List<TransportStatusDto>> GetAllStatusAsync()
        {

            return await _httpClient.GetFromJsonAsync<List<TransportStatusDto>>("api/TransportStatus/getAll");
        }

        public async Task<TransportStatusDto> CreateStatusAsync(TransportStatusUpdateAndCreateDto create)
        {
            var response = await _httpClient.PostAsJsonAsync("api/TransportStatus/create", create);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TransportStatusDto>();
        }

        public async Task<TransportStatusDto?> UpdateStatusAsync(int id, TransportStatusUpdateAndCreateDto update)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/TransportStatus/{id}", update);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TransportStatusDto>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                response.EnsureSuccessStatusCode();
                return null;
            }
        }

    }
}
