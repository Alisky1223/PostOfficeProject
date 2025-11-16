using CommonDll.Dto;
using PostOfficeProject.Core.src.Application.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IPostOfficeMiddelware
    {
        Task<ApiResponse<PostOfficeDto>> GetByIdAsync(int id);
        Task<ApiResponse<List<PostOfficeBasicInformationDto>>> GetAllPostOfficesAsync();
        Task<ApiResponse<PostOfficeDto>> UpdatePostOfficeAsync(int id, PostOfficeUpdateAndCreateDto updateDto);
        Task<ApiResponse<PostOfficeDto>> CreatePostOfficeAsync(PostOfficeUpdateAndCreateDto createDto);
    }
}
