namespace CommonDll.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string CustomerNumber { get; set; } = string.Empty;
        public int UserId { get; set; }
        public List<ProductDto> Products { get; set; } = [];
    }
}
