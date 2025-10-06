using Microsoft.EntityFrameworkCore;
using PostOfficeBackendProject.src.Domain.Interface;
using PostOfficeBackendProject.src.Domain.Model;
using PostOfficeBackendProject.src.Infrastructure.Data;

namespace PostOfficeBackendProject.src.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ProductRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var newProduct = await _dbContext.Product.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return newProduct.Entity;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var targetProduct = await _dbContext.Product.FirstOrDefaultAsync(x => x.Id == id);
            if (targetProduct == null) return null;

            targetProduct.Id = id;
            targetProduct.Price = product.Price;
            targetProduct.Description = product.Description;
            targetProduct.ProductName = product.ProductName;
            targetProduct.PostOfficeId = product.PostOfficeId;
            targetProduct.ProductTypeId = product.ProductTypeId;

            await _dbContext.SaveChangesAsync();
            return targetProduct;

        }
    }
}
