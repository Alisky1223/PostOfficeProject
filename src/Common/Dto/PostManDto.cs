namespace CommonDll.Dto
{
    public class PostManDto
    {
        public int Id { get; set; }
        public string PersonalCode { get; set; } = string.Empty;
        public int UserId { get; set; }
        public List<ProductDto> Products { get; set; } = [];
    }
}
