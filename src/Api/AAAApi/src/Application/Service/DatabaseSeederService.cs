using AAA.src.Domain.Interface;

namespace AAA.src.Application.Service
{
    public class DatabaseSeederService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        public DatabaseSeederService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();
            await SeedSuperAdminAsync();
        }

        private async Task SeedRolesAsync()
        {
            await _roleRepository.SeedRolesAsync();
        }

        private async Task SeedSuperAdminAsync()
        {
            await _userRepository.SeedSuperAdmin();
        }
    }
}
