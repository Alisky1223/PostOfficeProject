using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IPostManMiddleware
    {
        Task<ApiResponse<List<PostManDto>>> GetAllPostMansAsync();
        Task<ApiResponse<PostManDto>> UpdatePostManAsync(int id, PostmanUpdateAndCreateDto update);
        Task<ApiResponse<PostManDto>> CreatePostManAsync(PostmanUpdateAndCreateDto create);
        Task<ApiResponse<PostManDto>> GetByIdAsync(int id);
    }
}
