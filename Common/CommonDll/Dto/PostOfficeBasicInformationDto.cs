namespace CommonDll.Dto
{
    public class PostOfficeBasicInformationDto
    {
        public int Id { get; set; }
        public string OfficeName { get; set; } = string.Empty;
        public string OfficeAccessCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int StorageCapacity { get; set; }
    }
}
