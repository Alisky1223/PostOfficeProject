using CommonDll.Dto;
using PostOfficeBackendProject.src.Application.Dto;
using PostOfficeFrontendProject__all_interactive.Helper;
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

        public async Task<ApiResponse<List<CustomerDto>>> GetAllCustomersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<List<CustomerDto>>>("api/Customer/GetAllCustomers") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<List<CustomerDto>>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<CustomerDto>>UpdateCustomersAsync(int id, CustomerUpdateAndCreateDto updateDto)
        {

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Customer/UpdateCustomer/{id}", updateDto);

                return await ApiHandler.HandleResponse<ApiResponse<CustomerDto>>(response, "Update failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<CustomerDto>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<string>> CreateCustomersAsync(CustomerUpdateAndCreateDto createDto)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Customer/CreateCustomer", createDto);

                return await ApiHandler.HandleResponse<ApiResponse<string>>(response, "Create failed");
            }
            catch (Exception e)
            {
                return new ApiResponse<string>(e.Message, 500);
            }

        }

        public async Task<ApiResponse<CustomerDto>> GetByIdAsync(int id)
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<ApiResponse<CustomerDto>>($"api/Customer/GetCustomerById/{id}") ?? throw new Exception("API Response Is Null");

            }
            catch (Exception e)
            {
                return new ApiResponse<CustomerDto>(e.Message, 500);
            }

        }

    }

}
