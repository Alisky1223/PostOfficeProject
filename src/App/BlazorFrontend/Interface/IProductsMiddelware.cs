using CommonDll.Dto;
using PostOfficeBackendProject.src.Application.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IProductsMiddelware
    {
        Task<ApiResponse<ProductDto>> GetByIdAsync(int id);
        Task<ApiResponse<List<ProductBasicInformationDto>>> GetAllProductsAsync();
        Task<ApiResponse<ProductDto>> UpdateProductAsync(int id, ProductUpdateAndCreateDto updateDto);
        Task<ApiResponse<ProductDto>> CreateProductAsync(ProductUpdateAndCreateDto createDto);
    }
}
