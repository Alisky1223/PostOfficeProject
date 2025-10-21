namespace CommonDll.Dto
{
    public class TransportBasicInformationDto
    {
        public int Id { get; set; }
        public string DeliverdTo { get; set; } = string.Empty;
        public DateTime DeliverdDate { get; set; }
    }
}
