namespace API_Shop_Online.Dto.v1.Store
{
    public class StoreArticleDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public string ImageUrl { get; set; }
        public int ArticleId { get; set; }
        public string ArticleCode { get; set; }
        public string ArticleDescription { get; set; }
        public decimal ArticlePrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
