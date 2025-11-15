using PostOfficeProject.Core.src.Domain.Model;

namespace PostOfficeProject.Core.src.Domain.Interface
{
    public interface IProductTypeRepository
    {
        Task<List<ProductType>> GetAllProductTypesAsync();
        Task<ProductType> CreateProductType(ProductType productType);
        Task<ProductType?> UpdateProductTypeAsync(int id, ProductType productType);
    }
}
