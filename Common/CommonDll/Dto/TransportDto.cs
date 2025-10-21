using PostOfficeBackendProject.src.Application.Dto;

namespace CommonDll.Dto
{
    public class TransportDto
    {
        public int Id { get; set; }
        public string DeliverdTo { get; set; } = string.Empty;
        public DateTime DeliverdDate { get; set; }
        public PostOfficeDto? PostOffice { get; set; }
        public ProductDto? Product { get; set; }
        public PostManDto? Postman { get; set; }
    }
}
