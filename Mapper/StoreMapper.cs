using API_Shop_Online.Dto.v1.Article;
using API_Shop_Online.Dto.v1.Customer;
using API_Shop_Online.Dto.v1.Sale;
using API_Shop_Online.Dto.v1.Store;
using API_Shop_Online.Models;
using AutoMapper;

namespace API_Shop_Online.Mapper
{
    public class StoreMapper: Profile
    {
        public StoreMapper()
        {
            CreateMap<CustomerSubmissionDto, Customer>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<StoreSubmissionDto, Store>();
            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<ArticleSubmissionDto, Article>();
            CreateMap<Article, ArticleDto>();
            CreateMap<StoreArticle, StoreArticleDto>();
            CreateMap<CustomerArticle, CustomerArticleDto>();
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleSubmissionDto, Sale>();
            CreateMap<SaleArticleSubmissionDto, SaleArticle>();
        }
    }
}
