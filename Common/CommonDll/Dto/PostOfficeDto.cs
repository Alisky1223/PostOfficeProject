using CommonDll.Dto;

namespace PostOfficeBackendProject.src.Application.Dto
{
    public class PostOfficeDto
    {
        public int Id { get; set; }
        public string OfficeName { get; set; } = string.Empty;
        public string OfficeAccessCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int StorageCapacity { get; set; }
        public List<ProductBasicInformationDto> Products { get; set; } = [];
        public List<PostmanBasicInformationDto> PostMans { get; set; } = [];
        public List<TransportDto> Transport { get; set; } = [];
    }
}
