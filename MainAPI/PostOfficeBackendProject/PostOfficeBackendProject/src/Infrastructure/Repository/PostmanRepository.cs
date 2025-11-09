using Microsoft.EntityFrameworkCore;
using PostOfficeBackendProject.src.Domain.Interface;
using PostOfficeBackendProject.src.Domain.Model;
using PostOfficeBackendProject.src.Infrastructure.Data;

namespace PostOfficeBackendProject.src.Infrastructure.Repository
{
    public class PostmanRepository : IPostmanRepository
    {
        private readonly ApplicationDBContext _context;
        public PostmanRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Postman> Create(Postman postman)
        {
            var allreadyCreated = await _context.Postman.FirstOrDefaultAsync(x => x.UserId == postman.UserId);

            if (allreadyCreated != null) return allreadyCreated;
            
            var newPostman = await _context.Postman.AddAsync(postman);
            await _context.SaveChangesAsync();
            return newPostman.Entity;
        }

        public async Task<Postman?> GetPostmanById(int id)
        {
            var postman = await _context.Postman
                //.Include(c => c.Products).ThenInclude(c => c.ProductType)
                //.Include(c => c.Products).ThenInclude(c => c.TransportStatus)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (postman == null) return null;

            return postman;
        }

        public async Task<Postman?> GetPostmanByUserId(int userId)
        {
            var postman = await _context.Postman
                .Include(c => c.Products).ThenInclude(c => c.ProductType)
                .Include(c => c.Products).ThenInclude(c => c.TransportStatus)
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (postman == null) return null;

            return postman;
        }

        public Task<List<Postman>> GetPostmen()
        {
            return _context.Postman.ToListAsync();
        }

        public async Task<Postman?> Update(int id, Postman postman)
        {
            var targetPostman = await _context.Postman.FirstOrDefaultAsync(x => x.Id==id);
            if (targetPostman == null) return null;

            targetPostman.PostOffice = postman.PostOffice;
            targetPostman.PersonalCode = postman.PersonalCode;
            targetPostman.Id = id;
            targetPostman.PostOfficeId = postman.PostOfficeId;

            await _context.SaveChangesAsync();
            return targetPostman;
        }
    }
}
