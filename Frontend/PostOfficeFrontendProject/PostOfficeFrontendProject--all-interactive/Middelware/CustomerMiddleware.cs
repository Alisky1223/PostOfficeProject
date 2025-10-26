using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class CustomerMiddleware
    {
        private readonly HttpClient _httpClient;

        public CustomerMiddleware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/product/getall");
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, ProductUpdateAndCreateDto updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/product/update/{id}", updateDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            return null;
        }

        public async Task<ProductDto?> CreateProductAsync(ProductUpdateAndCreateDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/product/create", createDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            return null;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/product/getbyId/{id}");
        }
    }
}
