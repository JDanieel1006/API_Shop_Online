namespace API_Shop_Online.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }  
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }    
        public decimal TotalSale { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<SaleArticle> SaleArticles { get; set; }
    }
}
