namespace AAA.src.Domain.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;

        //Navigator
        public int RoleId { get; set; } 
        public Role? Role { get; set; } 

        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockedUntil { get; set; } // null = not locked
    }
}
