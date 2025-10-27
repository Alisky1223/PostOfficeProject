using AAA.src.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace AAA.src.Infrastructure.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        private ApplicationDBContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            return new ApplicationDBContext(options);
        }

        public DbSet<User> Users { get; set; }
    }
}
