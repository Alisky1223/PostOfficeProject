using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IProductDropdownMiddelware
    {
        //type
        Task<List<ProductTypeDto>> GetAllProductTypeAsync();
        Task<ProductTypeDto> CreateProductTypeAsync(ProductTypeUpdateAndCreateDto createDto);
        Task<ProductTypeDto?> UpdateProductTypeAsync(int id, ProductTypeUpdateAndCreateDto updateDto);
        //status
        Task<List<TransportStatusDto>> GetAllStatusAsync();
        Task<TransportStatusDto> CreateStatusAsync(TransportStatusUpdateAndCreateDto create);
        Task<TransportStatusDto?> UpdateStatusAsync(int id, TransportStatusUpdateAndCreateDto update);
    }
}
