using API_Shop_Online.Common.Enum;

namespace API_Shop_Online.Dto.v1.Customer
{
    public class CustomerArticleSubmissionDto
    {
        public int ArticleId { get; set; }
        public CustomerArticleStatus Status { get; set; }
    }
}
