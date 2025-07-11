using API_Shop_Online.Common.Enum;
using API_Shop_Online.Data;
using API_Shop_Online.Dto.v1.Sale;
using API_Shop_Online.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Shop_Online.Services.Sales
{
    public class SalesService : ISalesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SalesService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SaleDto> RegisterSaleAsync(SaleSubmissionDto dto)
        {
            var totalSale = dto.Articles.Sum(a => a.UnitPrice * a.Quantity);

            var sale = new Sale
            {
                StoreId = dto.StoreId,
                CustomerId = dto.CustomerId,
                TotalSale = totalSale,
                CreatedAt = DateTime.UtcNow,
                SaleArticles = dto.Articles.Select(a => new SaleArticle
                {
                    ArticleId = a.ArticleId,
                    Quantity = a.Quantity,
                    UnitPrice = a.UnitPrice,
                    Subtotal = a.UnitPrice * a.Quantity
                }).ToList()
            };

            _dbContext.Sales.Add(sale);

            var soldArticleIds = dto.Articles.Select(a => a.ArticleId).ToList();

            var customerArticles = await _dbContext.CustomerArticles
                .Where(ca => ca.CustomerId == dto.CustomerId && soldArticleIds.Contains(ca.ArticleId))
                .ToListAsync();

            foreach (var ca in customerArticles)
            {
                ca.Status = CustomerArticleStatus.Sold;
            }

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SaleDto>(sale);
        }
    }
}
