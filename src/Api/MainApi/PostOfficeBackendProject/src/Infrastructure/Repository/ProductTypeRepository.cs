using Microsoft.EntityFrameworkCore;
using PostOfficeProject.Core.src.Domain.Interface;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Data;

namespace PostOfficeProject.Core.src.Infrastructure.Repository
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductTypeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ProductType> CreateProductType(ProductType productType)
        {
            var newProductType = await _context.ProductType.AddAsync(productType);
            await _context.SaveChangesAsync();
            return newProductType.Entity;
        }

        public async Task<List<ProductType>> GetAllProductTypesAsync()
        {
            return await _context.ProductType.ToListAsync();
        }

        public async Task<ProductType?> UpdateProductTypeAsync(int id, ProductType productType)
        {
            var targetProductType = await _context.ProductType.FirstOrDefaultAsync(x => x.Id == id);
            if (targetProductType == null) return null;

            targetProductType.Id = id;
            targetProductType.Type = productType.Type;

            await _context.SaveChangesAsync();
            return targetProductType;
        }
    }
}
