using API_Shop_Online.Dto.v1.Store;
using API_Shop_Online.Models;

namespace API_Shop_Online.Services.Store
{
    public interface IStoreService
    {
        ICollection<API_Shop_Online.Models.Store> Get();

        API_Shop_Online.Models.Store? GetById(int id);

        Task<API_Shop_Online.Models.Store> Create(StoreSubmissionDto request);

        Task<API_Shop_Online.Models.Store> Update(int id, StoreSubmissionDto request);

        void Delete(int id);
    }
}
