using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Domain.Interface
{
    public interface ITransportStatusRepository
    {
        Task<List<TransportStatus>> GetAll();
        Task<TransportStatus?> GetById(int id);
        Task<TransportStatus> Create(TransportStatus transportStatus);
        Task<TransportStatus?> Update(int id, TransportStatus transportStatus);
        Task SeedTransportStatus();
    }
}
