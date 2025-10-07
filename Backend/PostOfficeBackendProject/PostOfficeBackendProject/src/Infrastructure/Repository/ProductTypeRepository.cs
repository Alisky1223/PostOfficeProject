using Microsoft.EntityFrameworkCore;
using PostOfficeBackendProject.src.Domain.Interface;
using PostOfficeBackendProject.src.Domain.Model;
using PostOfficeBackendProject.src.Infrastructure.Data;

namespace PostOfficeBackendProject.src.Infrastructure.Repository
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
