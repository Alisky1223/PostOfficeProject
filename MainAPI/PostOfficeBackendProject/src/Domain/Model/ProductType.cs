namespace PostOfficeBackendProject.src.Domain.Model
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public List<Product> Products { get; set;} = new();
    }
}
