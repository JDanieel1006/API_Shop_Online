namespace API_Shop_Online.Dto.v1.Article
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } 
    }
}
