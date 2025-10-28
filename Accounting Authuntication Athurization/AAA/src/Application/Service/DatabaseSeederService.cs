using AAA.src.Domain.Interface;

namespace AAA.src.Application.Service
{
    public class DatabaseSeederService
    {
        private readonly IRoleRepository _roleRepository;
        public DatabaseSeederService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task SeedAsync() 
        {
            await SeedRolesAsync();
        }

        private async Task SeedRolesAsync() 
        {
            await _roleRepository.SeedRolesAsync();
        }
    }
}
