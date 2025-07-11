namespace API_Shop_Online.Dto.v1.Sale
{
    public class SaleSubmissionDto
    {
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public List<SaleArticleSubmissionDto> Articles { get; set; }
    }
}
