namespace CommonDll.Dto
{
    public class TransportDto
    {
        public int Id { get; set; }
        public string DeliverCode { get; set; } = string.Empty;
        public DateTime DeliverdDate { get; set; }
        public PostOfficeBasicInformationDto? PostOffice { get; set; }
        public ProductBasicInformationDto? Product { get; set; }
        public PostmanBasicInformationDto? Postman { get; set; }
        public CustomerBasicInformationDto? Customer { get; set; }
    }
}
