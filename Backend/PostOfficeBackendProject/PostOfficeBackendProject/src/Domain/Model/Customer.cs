namespace PostOfficeBackendProject.src.Domain.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CustomerNumber { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = [];
    }
}
