namespace API_Shop_Online.Models
{
    public class Articles
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }

        public ICollection<StoreArticle> StoreArticles { get; set; }
        public ICollection<CustomerArticles> CustomerArticles { get; set; }
    }
}
