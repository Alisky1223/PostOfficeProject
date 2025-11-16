namespace CommonDll.Dto
{
    public class UserCustomerPostmanDto
    {
        public string PersonalCode { get; set; } = string.Empty;
        //personal information
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;

        public List<ProductDto> Products { get; set; } = [];

    }
}
