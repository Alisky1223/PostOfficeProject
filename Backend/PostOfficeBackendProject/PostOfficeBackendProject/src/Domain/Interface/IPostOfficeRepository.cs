using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Domain.Interface
{
    public interface IPostOfficeRepository
    {
        Task<List<PostOffice>> GetAllPostsAsync();
        Task<PostOffice?> GetPostByIdAsync(int id);
        Task<PostOffice> CreatePostAsync(PostOffice postOffice);
        Task<PostOffice?> UpdatePostAsync(int Id, PostOffice postOffice);
    }
}
