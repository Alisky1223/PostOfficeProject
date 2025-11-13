using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface ICustomerMiddleware
    {
        Task<ApiResponse<List<CustomerDto>>> GetAllCustomersAsync();
        Task<ApiResponse<CustomerDto>> UpdateCustomersAsync(int id, CustomerUpdateAndCreateDto updateDto);
        Task<ApiResponse<CustomerDto>> CreateCustomersAsync(CustomerUpdateAndCreateDto createDto);
        Task<ApiResponse<CustomerDto>> GetByIdAsync(int id);

    }
}
