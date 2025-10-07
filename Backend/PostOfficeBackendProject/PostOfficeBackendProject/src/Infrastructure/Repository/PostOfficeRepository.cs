using Microsoft.EntityFrameworkCore;
using PostOfficeBackendProject.src.Domain.Interface;
using PostOfficeBackendProject.src.Domain.Model;
using PostOfficeBackendProject.src.Infrastructure.Data;

namespace PostOfficeBackendProject.src.Infrastructure.Repository
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
                .Include(c => c.Products).ThenInclude(c => c.ProductType)
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
