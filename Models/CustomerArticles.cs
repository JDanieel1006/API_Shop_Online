namespace API_Shop_Online.Models
{
    public class CustomerArticle
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public DateTime Fecha { get; set; }
    }
}
