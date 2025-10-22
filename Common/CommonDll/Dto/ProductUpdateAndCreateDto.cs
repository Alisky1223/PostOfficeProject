namespace CommonDll.Dto
{
    public class ProductUpdateAndCreateDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? ProductTypeId { get; set; }
        public int? PostOfficeId { get; set; }
        public int? TransportStatusId { get; set; }
    }
}
