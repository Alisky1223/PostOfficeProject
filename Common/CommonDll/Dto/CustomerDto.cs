namespace CommonDll.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CustomerNumber { get; set; } = string.Empty;

        public List<ProductDto> Products { get; set; } = [];
    }
}
