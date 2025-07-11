namespace API_Shop_Online.Dto.v1.Customer
{
    public class CustomerSubmissionDto
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public required string Address { get; set; }
    }
}
