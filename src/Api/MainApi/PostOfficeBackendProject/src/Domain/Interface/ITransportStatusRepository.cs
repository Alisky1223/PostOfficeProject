using PostOfficeProject.Core.src.Domain.Model;

namespace PostOfficeProject.Core.src.Domain.Interface
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
