using PostOfficeProject.Core.src.Domain.Model;

namespace PostOfficeProject.Core.src.Domain.Interface
{
    public interface IPostmanRepository
    {
        Task<List<Postman>> GetPostmen();
        Task<Postman?> GetPostmanById(int id);
        Task<Postman?> GetPostmanByUserId(int userId);
        Task<Postman> Create(Postman postman);  
        Task<Postman?> Update(int id, Postman postman);
    }
}
