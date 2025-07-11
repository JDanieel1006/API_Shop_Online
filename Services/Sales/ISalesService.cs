using API_Shop_Online.Dto.v1.Sale;

namespace API_Shop_Online.Services.Sales
{
    public interface ISalesService
    {
        Task<SaleDto> RegisterSaleAsync(SaleSubmissionDto dto);

    }
}
