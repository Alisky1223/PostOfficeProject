namespace AAA.src.Domain.Model
{
    public class LoginAttempt
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public bool Success { get; set; }
        public DateTime AttemptedAt { get; set; }
        public string IpAddress { get; set; } = string.Empty;
    }
}
