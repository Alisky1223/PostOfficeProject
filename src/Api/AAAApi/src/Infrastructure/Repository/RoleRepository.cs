using AAA.src.Domain.Interface;
using AAA.src.Domain.Model;
using AAA.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AAA.src.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDBContext _context;
        public RoleRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Role> AddRoles(Role role)
        {
            var newRole = await _context.Role.AddAsync(role);
            await _context.SaveChangesAsync();
            return newRole.Entity;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task SeedRolesAsync()
        {
            if (await _context.Role.AnyAsync()) return;

            List<Role> roles = [];
            roles.Add(new Role
            {
                Name = "SuperAdmin"
            });
            roles.Add(new Role
            {
                Name = "Admin"
            });
            roles.Add(new Role
            {
                Name = "Customer"
            });
            roles.Add(new Role
            {
                Name = "Postman"
            });
            roles.Add(new Role
            {
                Name = "User"
            });

            await _context.Role.AddRangeAsync(roles);
            await _context.SaveChangesAsync();

        }
    }
}
