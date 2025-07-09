namespace API_Shop_Online.Dto.v1.Article
{
    public class ArticleSubmissionDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        public int Stock { get; set; }
    }
}
