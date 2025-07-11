namespace API_Shop_Online.Models
{
    public class SaleArticle
    {
        public int Id { get; set; }

        public int SaleId { get; set; }
        public Sale Sale { get; set; }  
        public int ArticleId { get; set; }
        public Article Article { get; set; }           
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}
