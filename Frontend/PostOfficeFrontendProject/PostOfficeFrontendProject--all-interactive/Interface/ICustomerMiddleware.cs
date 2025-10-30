using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface ICustomerMiddleware
    {
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> UpdateCustomersAsync(int id, CustomerUpdateAndCreateDto updateDto);
        Task<CustomerDto?> CreateCustomersAsync(CustomerUpdateAndCreateDto createDto);
        Task<CustomerDto?> GetByIdAsync(int id);
    }
}
