using AAA.src.Domain.Model;
using CommonDll.Dto;

namespace AAA.src.Domain.Interface
{
    public interface IUserRepository
    {
        Task<LoginResultDto> LoginAsync(LoginDto loginDto, string ipAddress);
        Task<string?> RegisterAsync(RegisterDto registerDto);
        Task<User?> ChangeUserRole(int id, int RoleId);
        Task<List<Role>> GetAllRoles();
        Task SeedSuperAdmin();
        Task<User?> GetUserById(int id);
        Task<List<User>> GetUsers();
    }
}
