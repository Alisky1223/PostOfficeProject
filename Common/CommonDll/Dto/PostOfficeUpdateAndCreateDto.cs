namespace PostOfficeBackendProject.src.Application.Dto
{
    public class PostOfficeUpdateAndCreateDto
    {
        public string OfficeName { get; set; } = string.Empty;
        public string OfficeAccessCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int StorageCapacity { get; set; }
    }
}
