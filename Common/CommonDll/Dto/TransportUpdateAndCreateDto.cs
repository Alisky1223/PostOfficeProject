namespace CommonDll.Dto
{
    public class TransportUpdateAndCreateDto
    {
        public string DeliverdTo { get; set; } = string.Empty;
        public DateTime DeliverdDate { get; set; }

        public int? PostOfficeId { get; set; }
        public int? ProductId { get; set; }
        public int PostmanId { get; set; }
    }
}
