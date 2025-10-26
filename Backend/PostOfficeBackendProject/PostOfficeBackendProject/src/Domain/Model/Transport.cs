namespace PostOfficeBackendProject.src.Domain.Model
{
    public class Transport
    {
        public int Id { get; set; }
        public string DeliverCode { get; set; } = string.Empty;
        public DateTime DeliverdDate { get; set; }

        public int? PostOfficeId { get; set; }
        public PostOffice? PostOffice { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int PostmanId { get; set; }
        public Postman? Postman { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
