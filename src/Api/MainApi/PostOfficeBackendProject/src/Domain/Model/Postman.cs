namespace PostOfficeProject.Core.src.Domain.Model
{
    public class Postman
    {
        public int Id { get; set; }
        public string PersonalCode { get; set; } = string.Empty;

        //Navigation
        public int? PostOfficeId { get; set; }
        public PostOffice? PostOffice { get; set; }

        public int UserId { get; set; }

        public List<Product> Products { get; set; } = [];
    }
}
