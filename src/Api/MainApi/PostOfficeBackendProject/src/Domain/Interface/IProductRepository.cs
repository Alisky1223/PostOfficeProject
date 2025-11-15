using PostOfficeProject.Core.src.Domain.Model;

namespace PostOfficeProject.Core.src.Domain.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(int id, Product product);
        Task<Product?> GetById(int id);
    }
}
