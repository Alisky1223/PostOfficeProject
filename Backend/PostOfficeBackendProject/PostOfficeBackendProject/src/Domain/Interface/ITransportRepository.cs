using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Domain.Interface
{
    public interface ITransportRepository
    {
        Task<List<Transport>> GetAllAsync();
        Task<Transport?> GetByIdAsync(int id);
        Task<Transport> CreateAsync(Transport transport);
        Task<Transport?> UpdateAsync(int id, Transport transport);
    }
}
