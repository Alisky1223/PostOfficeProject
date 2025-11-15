namespace PostOfficeProject.Core.src.Domain.Model
{
    public class TransportStatus
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<Product> Product { get; set; } = [];
    }
}
