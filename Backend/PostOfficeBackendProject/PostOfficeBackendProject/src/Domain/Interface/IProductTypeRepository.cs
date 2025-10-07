using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Domain.Interface
{
    public interface IProductTypeRepository
    {
        Task<List<ProductType>> GetAllProductTypesAsync();
        Task<ProductType> CreateProductType(ProductType productType);
        Task<ProductType?> UpdateProductTypeAsync(int id, ProductType productType);
    }
}
