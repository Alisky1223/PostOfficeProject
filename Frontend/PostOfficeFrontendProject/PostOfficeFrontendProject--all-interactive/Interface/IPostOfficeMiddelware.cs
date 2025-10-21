using PostOfficeBackendProject.src.Application.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IPostOfficeMiddelware
    {
        Task<List<PostOfficeDto>> GetAllPostOfficesAsync();
        
    }
}
