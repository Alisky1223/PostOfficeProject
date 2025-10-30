using CommonDll.Dto;
using PostOfficeFrontendProject__all_interactive.Interface;

namespace PostOfficeFrontendProject__all_interactive.Middelware
{
    public class CustomerMiddleware : ICustomerMiddleware
    { 
        private readonly HttpClient _httpClient;

        public CustomerMiddleware(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/Customer/GetAllCustomers");
        }

        public async Task<CustomerDto?> UpdateCustomersAsync(int id, CustomerUpdateAndCreateDto updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Customer/UpdateCustomer/{id}", updateDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CustomerDto>();
            }
            return null;
        }

        public async Task<CustomerDto?> CreateCustomersAsync(CustomerUpdateAndCreateDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Customer/CreateCustomer", createDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CustomerDto>();
            }
            return null;
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/Customer/GetCustomerById/{id}");
        }
    }
}
