using Microsoft.EntityFrameworkCore;
using PostOfficeBackendProject.src.Domain.Interface;
using PostOfficeBackendProject.src.Domain.Model;
using PostOfficeBackendProject.src.Infrastructure.Data;

namespace PostOfficeBackendProject.src.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDBContext _context;
        public CustomerRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Customer> CreateAsync(Customer customer)
        {
            var newCustomer = await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return newCustomer.Entity;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            var targetCustomer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == id);

            if (targetCustomer == null) return null;

            return targetCustomer;
        }

        public async Task<Customer?> UpdateCustomerAsync(int id, Customer customer)
        {
            var targetCustomer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == id);

            if (targetCustomer == null) return null;

            targetCustomer.Id = id;
            targetCustomer.Name = customer.Name;
            targetCustomer.CustomerNumber = customer.CustomerNumber;

            await _context.SaveChangesAsync();

            return targetCustomer;
        }
    }
}
