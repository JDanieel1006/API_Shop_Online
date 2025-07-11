namespace API_Shop_Online.Dto.v1.Store
{
    public class StoreSubmissionDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
