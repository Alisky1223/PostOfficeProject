using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Domain.Interface
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
