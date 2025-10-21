using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IProductsMiddelware
    {
        Task<ProductDto?> GetByIdAsync(int id);
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> CreateProductAsync(ProductUpdateAndCreateDto createDto);
        Task<ProductDto?> UpdateProductAsync(int id, ProductUpdateAndCreateDto updateDto);
    }
}
