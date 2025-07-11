using API_Shop_Online.Dto.v1.Store;
using API_Shop_Online.Models;

namespace API_Shop_Online.Services.Store
{
    public interface IStoreService
    {
        ICollection<API_Shop_Online.Models.Store> Get();

        API_Shop_Online.Models.Store? GetById(int id);

        Task<StoreArticleDto?> GetStoreArticle(int storeId, int articleId);

        Task<List<StoreArticleDto>> GetArticlesByStore(int storeId, string baseUrl);

        Task<API_Shop_Online.Models.Store> Create(StoreSubmissionDto request);

        Task<StoreArticleDto> AddArticleToStore(int storeId, StoreArticleSubmissionDto dto);

        Task<API_Shop_Online.Models.Store> Update(int id, StoreSubmissionDto request);

        void Delete(int id);

        
      
    }
}
