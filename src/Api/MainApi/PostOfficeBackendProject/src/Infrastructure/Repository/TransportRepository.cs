using Microsoft.EntityFrameworkCore;
using PostOfficeProject.Core.src.Domain.Interface;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Data;

namespace PostOfficeProject.Core.src.Infrastructure.Repository
{
    public class TransportRepository : ITransportRepository
    {
        private readonly ApplicationDBContext _context;
        public TransportRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Transport> CreateAsync(Transport transport)
        {
            var newTransport = await _context.Transport.AddAsync(transport);
            await _context.SaveChangesAsync();
            return newTransport.Entity;
        }

        public async Task<List<Transport>> GetAllAsync()
        {
            return await _context.Transport
                .Include(c => c.Postman)
                .Include(c => c.PostOffice)
                .Include(c => c.Product)
                .Include(c => c.Customer)
                .ToListAsync();
        }

        public async Task<Transport?> GetByIdAsync(int id)
        {
            var transport = await _context.Transport
                .Include(c => c.Postman)
                .Include(c => c.PostOffice)
                .Include(c => c.Product)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (transport == null) return null;

            return transport;
        }

        public async Task<Transport?> UpdateAsync(int id, Transport transport)
        {
            var targetTransport = await _context.Transport
                .Include(c => c.Postman)
                .Include(c => c.PostOffice)
                .Include(c => c.Product)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (targetTransport == null) return null;

            targetTransport.Id = id;    
            targetTransport.ProductId = transport.ProductId;
            targetTransport.PostOfficeId = transport.PostOfficeId;
            targetTransport.PostmanId = transport.PostmanId;
            targetTransport.CustomerId = transport.CustomerId;
            targetTransport.DeliverdDate = transport.DeliverdDate;
            targetTransport.DeliverCode = transport.DeliverCode;

            await _context.SaveChangesAsync();
            return targetTransport;
        }
    }
}
