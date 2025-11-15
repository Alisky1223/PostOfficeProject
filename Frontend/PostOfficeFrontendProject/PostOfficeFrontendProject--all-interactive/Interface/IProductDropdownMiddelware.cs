using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IProductDropdownMiddelware
    {
        //type
        Task<ApiResponse<List<ProductTypeDto>>> GetAllProductTypeAsync();
        Task<ApiResponse<ProductTypeDto>> CreateProductTypeAsync(ProductTypeUpdateAndCreateDto createDto);
        Task<ApiResponse<ProductTypeDto>> UpdateProductTypeAsync(int id, ProductTypeUpdateAndCreateDto updateDto);

        //status
        Task<ApiResponse<List<TransportStatusDto>>> GetAllStatusAsync();
        Task<ApiResponse<TransportStatusDto>> CreateStatusAsync(TransportStatusUpdateAndCreateDto create);
        Task<ApiResponse<TransportStatusDto>> UpdateStatusAsync(int id, TransportStatusUpdateAndCreateDto update);
    }
}
