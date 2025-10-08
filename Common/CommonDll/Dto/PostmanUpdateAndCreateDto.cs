namespace CommonDll.Dto
{
    public class PostmanUpdateAndCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string PersonalCode { get; set; } = string.Empty;
        public int? PostOfficeId { get; set; }
    }
}
