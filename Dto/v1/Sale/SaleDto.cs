namespace API_Shop_Online.Dto.v1.Sale
{
    public class SaleDto
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalSale { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
