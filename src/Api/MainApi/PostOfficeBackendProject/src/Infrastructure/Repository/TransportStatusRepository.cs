using Microsoft.EntityFrameworkCore;
using PostOfficeProject.Core.src.Domain.Interface;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Data;

namespace PostOfficeProject.Core.src.Infrastructure.Repository
{
    public class TransportStatusRepository : ITransportStatusRepository
    {
        public readonly ApplicationDBContext _context;
        public TransportStatusRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<TransportStatus> Create(TransportStatus transportStatus)
        {
            var newStatus = await _context.TransportStatus.AddAsync(transportStatus);
            await _context.SaveChangesAsync();
            return newStatus.Entity;
        }

        public async Task<List<TransportStatus>> GetAll()
        {
            return await _context.TransportStatus.ToListAsync();
        }

        public async Task<TransportStatus?> GetById(int id)
        {
            var targetStatus = await _context.TransportStatus.FirstOrDefaultAsync(x => x.Id == id);
            if (targetStatus == null) return null;
            return targetStatus;
        }

        public async Task SeedTransportStatus()
        {
            if (await _context.TransportStatus.AnyAsync()) return;

            var instation = new TransportStatus { Status = "In Station" };
            var postmanDelivering = new TransportStatus { Status = "Postman Delivering" };
            var postmanDelivered = new TransportStatus { Status = "Postman Delivered" };

            await _context.TransportStatus.AddAsync(instation);
            await _context.TransportStatus.AddAsync(postmanDelivering);
            await _context.TransportStatus.AddAsync(postmanDelivered);

            await _context.SaveChangesAsync();
        }

        public async Task<TransportStatus?> Update(int id, TransportStatus transportStatus)
        {
            var targetStatus = await _context.TransportStatus.FirstOrDefaultAsync(x => x.Id == id);
            if (targetStatus == null) return null;

            targetStatus.Id = id;
            targetStatus.Status = transportStatus.Status;

            await _context.SaveChangesAsync();

            return targetStatus;
        }
    }
}
