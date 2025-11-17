using AAA.src.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace AAA.src.Infrastructure.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<LoginAttempt> LoginAttempts { get; set; }
        public DbSet<Role> Role { get; set; }

    }
}
