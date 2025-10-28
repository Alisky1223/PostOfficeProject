using AAA.src.Domain.Model;

namespace AAA.src.Domain.Interface
{
    public interface IRoleRepository
    {
        Task SeedRolesAsync();
        Task<Role> AddRoles(Role role);
        Task<List<Role>> GetRolesAsync();
    }
}
