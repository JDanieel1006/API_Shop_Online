namespace API_Shop_Online.Models
{
    public class StoreArticle
    {
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
