namespace PostOfficeBackendProject.src.Domain.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public int? PostOfficeId { get; set; }
        public PostOffice? PostOffice { get; set; }
        
        public int? ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }

        public int? TransportStatusId { get; set; }
        public TransportStatus? TransportStatus { get; set; }

        public int? PostmanId { get; set; }
        public Postman? Postman { get; set; }
    }
}
