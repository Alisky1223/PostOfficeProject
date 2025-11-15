namespace AAA.src.Domain.Model
{
    public class PasswordPolicy
    {
        public bool RequireStrongPassword { get; set; } = true;
        public int MinLength { get; set; } = 8;
        public bool RequireUppercase { get; set; } = true;
        public bool RequireLowercase { get; set; } = true;
        public bool RequireDigit { get; set; } = true;
        public bool RequireSpecialChar { get; set; } = true;
        public string SpecialChars { get; set; } = "!@#$%^&*()_+-=[]{}|;:,.<>?";
    }
}
