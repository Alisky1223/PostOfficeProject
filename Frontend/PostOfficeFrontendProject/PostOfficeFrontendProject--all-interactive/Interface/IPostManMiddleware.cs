using CommonDll.Dto;

namespace PostOfficeFrontendProject__all_interactive.Interface
{
    public interface IPostManMiddleware
    {
        Task<List<PostManDto>> GetAllPostMansAsync();
        Task<PostManDto?> UpdatePostManAsync(int id, PostmanUpdateAndCreateDto update);
        Task<PostManDto?> CreatePostManAsync(PostmanUpdateAndCreateDto create);
        Task<PostManDto?> GetByIdAsync(int id);
    }
}
