using API_Shop_Online.Common.Enum;

namespace API_Shop_Online.Dto.v1.Customer
{
    public class CustomerArticleDto
    {
        public int CustomerId { get; set; }
        public int ArticleId { get; set; }
        public decimal ArticlePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public CustomerArticleStatus Status { get; set; }
    }
}
