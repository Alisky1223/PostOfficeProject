using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Domain.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer?> GetCustomerByUserIdAsync(int id);
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer?> UpdateCustomerAsync(int id, Customer customer);
    }
}
