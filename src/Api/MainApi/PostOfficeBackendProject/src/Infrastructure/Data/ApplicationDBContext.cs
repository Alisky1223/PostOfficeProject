using Microsoft.EntityFrameworkCore;
using PostOfficeProject.Core.src.Domain.Model;

namespace PostOfficeProject.Core.src.Infrastructure.Data
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

        public DbSet<PostOffice> PostOffice { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Postman> Postman { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public DbSet<TransportStatus> TransportStatus { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
