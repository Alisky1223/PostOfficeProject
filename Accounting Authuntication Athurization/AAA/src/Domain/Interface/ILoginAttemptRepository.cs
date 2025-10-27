using AAA.src.Domain.Model;

namespace AAA.src.Domain.Interface
{
    public interface ILoginAttemptRepository
    {
        Task SuccessfullAttempt(LoginAttempt loginAttempt);
        Task UnSuccessfullAttempt(LoginAttempt loginAttempt);
    }
}
