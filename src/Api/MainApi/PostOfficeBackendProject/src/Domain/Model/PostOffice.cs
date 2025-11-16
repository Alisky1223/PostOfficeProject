namespace PostOfficeProject.Core.src.Domain.Model
{
    public class PostOffice
    {
        public int Id { get; set; }
        public string OfficeName { get; set; } = string.Empty;
        public string OfficeAccessCode { get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public int StorageCapacity { get; set; }

        //Navigation
        public List<Product> Products { get; set; } = [];
        public List<Postman> Postman { get; set; } = [];
        public List<Transport> Transport { get; set; } = [];
    }
}
