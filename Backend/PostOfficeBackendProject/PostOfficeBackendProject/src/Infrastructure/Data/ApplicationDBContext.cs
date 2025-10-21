using Microsoft.EntityFrameworkCore;
using PostOfficeBackendProject.src.Domain.Model;

namespace PostOfficeBackendProject.src.Infrastructure.Data
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

        internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public DbSet<PostOffice> PostOffice { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Postman> Postman { get; set; }
    }
}
