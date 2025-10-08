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
        public List<ProductDto> Products { get; set; } = [];
        public List<PostManDto> PostMans { get; set; } = [];
    }
}
