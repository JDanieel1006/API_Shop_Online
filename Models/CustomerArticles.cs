namespace API_Shop_Online.Models
{
    public class CustomerArticles
    {
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }

        public int ArticleId { get; set; }
        public Articles Article { get; set; }

        public DateTime Fecha { get; set; }
    }
}
