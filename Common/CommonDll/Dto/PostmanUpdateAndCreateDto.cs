namespace CommonDll.Dto
{
    public class PostmanUpdateAndCreateDto
    {
        public string PersonalCode { get; set; } = string.Empty;
        public int? PostOfficeId { get; set; }
        public int UserId { get; set; }
    }
}
