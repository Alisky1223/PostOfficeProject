using AAA.src.Domain.Interface;
using AAA.src.Domain.Model;
using AAA.src.Infrastructure.Data;

namespace AAA.src.Infrastructure.Repository
{
    public class LoginAttemptRepository : ILoginAttemptRepository
    {
        private readonly ApplicationDBContext _context;
        public LoginAttemptRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task SuccessfullAttempt(LoginAttempt loginAttempt)
        {
            loginAttempt.Success = true;
            _context.LoginAttempts.Add(loginAttempt);
            await _context.SaveChangesAsync();

        }

        public async Task UnSuccessfullAttempt(LoginAttempt loginAttempt)
        {
            loginAttempt.Success = false;
            _context.LoginAttempts.Add(loginAttempt);
            await _context.SaveChangesAsync();
        }
    }
}
