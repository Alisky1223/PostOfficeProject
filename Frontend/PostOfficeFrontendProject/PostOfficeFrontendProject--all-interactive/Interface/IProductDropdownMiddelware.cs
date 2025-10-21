using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IProductDropdownMiddelware
    {
        Task<List<ProductTypeDto>> GetAllProductTypeAsync();
        Task<ProductTypeDto> CreateProductTypeAsync(ProductTypeUpdateAndCreateDto createDto);
        Task<ProductTypeDto?> UpdateProductTypeAsync(int id, ProductTypeUpdateAndCreateDto updateDto);
    }
}
