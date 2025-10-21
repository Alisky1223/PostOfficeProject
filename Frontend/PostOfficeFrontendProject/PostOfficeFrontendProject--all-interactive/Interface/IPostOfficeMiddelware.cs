using CommonDll.Dto;
using PostOfficeBackendProject.src.Application.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IPostOfficeMiddelware
    {
        Task<PostOfficeDto?> GetByIdAsync(int id);
        Task<List<PostOfficeBasicInformationDto>> GetAllPostOfficesAsync();
        Task<PostOfficeDto?> UpdatePostOfficeAsync(int id, PostOfficeUpdateAndCreateDto updateDto);
        Task<PostOfficeDto?> CreatePostOfficeAsync(PostOfficeUpdateAndCreateDto createDto);
    }
}
