using Microsoft.EntityFrameworkCore;
using PostOfficeProject.Core.src.Domain.Interface;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Data;

namespace PostOfficeProject.Core.src.Infrastructure.Repository
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
            try
            {
                var newProduct = await _dbContext.Product.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return newProduct.Entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            var targetProduct = await _dbContext.Product
                .Include(c => c.ProductType)
                .Include(c => c.TransportStatus)
                .Include(c => c.Postman)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (targetProduct == null) return null;

            return targetProduct;
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
            targetProduct.TransportStatusId = product.TransportStatusId;
            targetProduct.PostmanId = product.PostmanId;
            targetProduct.CustomerId = product.CustomerId;

            await _dbContext.SaveChangesAsync();
            return targetProduct;

        }
    }
}
