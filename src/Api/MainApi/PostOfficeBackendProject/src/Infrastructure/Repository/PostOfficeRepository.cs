using Microsoft.EntityFrameworkCore;
using PostOfficeProject.Core.src.Domain.Interface;
using PostOfficeProject.Core.src.Domain.Model;
using PostOfficeProject.Core.src.Infrastructure.Data;

namespace PostOfficeProject.Core.src.Infrastructure.Repository
{
    public class PostOfficeRepository : IPostOfficeRepository
    {
        private readonly ApplicationDBContext _context;
        public PostOfficeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<PostOffice> CreatePostAsync(PostOffice postOffice)
        {
            var newPostOffice = await _context.PostOffice.AddAsync(postOffice);
            await _context.SaveChangesAsync();
            return newPostOffice.Entity;
        }

        public async Task<List<PostOffice>> GetAllPostsAsync()
        {
            return await _context.PostOffice.ToListAsync();
        }

        public async Task<PostOffice?> GetPostByIdAsync(int id)
        {
            return await _context.PostOffice
                //.Include(c => c.Products).ThenInclude(c => c.ProductType)
                //.Include(c => c.Products).ThenInclude(c => c.TransportStatus)
                //.Include(c => c.Products).ThenInclude(c => c.Postman)
                //.Include(c => c.Products).ThenInclude(c => c.Customer)
                .Include(c => c.Products)
                .Include(c => c.Postman)
                .Include(c => c.Transport).ThenInclude(c => c.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PostOffice?> UpdatePostAsync(int Id, PostOffice postOffice)
        {
            var targetPostOffice = await _context.PostOffice.FirstOrDefaultAsync(x => x.Id == Id);

            if (targetPostOffice == null) return null;

            targetPostOffice.OfficeName = postOffice.OfficeName;
            targetPostOffice.OfficeAccessCode = postOffice.OfficeAccessCode;
            targetPostOffice.Address = postOffice.Address;
            targetPostOffice.StorageCapacity = postOffice.StorageCapacity;

            await _context.SaveChangesAsync();

            return targetPostOffice;
        }
    }
}
