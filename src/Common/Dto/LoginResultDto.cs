namespace CommonDll.Dto
{
    public class LoginResultDto
    {
        public int StatusCode { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
        public DateTime? LockedUntil { get; set; }
    }
}
