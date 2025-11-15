using PostOfficeProject.Core.src.Domain.Interface;

namespace PostOfficeProject.Core.src.Application.Service
{
    public class DatabaseSeederService
    {
        private readonly ITransportStatusRepository _transportStatusRepository;
        public DatabaseSeederService(ITransportStatusRepository transportStatusRepository)
        {
            _transportStatusRepository = transportStatusRepository;
        }

        public async Task SeedAsync() 
        {
            await SeedTransportStatus();
        }

        private async Task SeedTransportStatus() => await _transportStatusRepository.SeedTransportStatus();
    }
}
