using API_Shop_Online.Data;
using API_Shop_Online.Dto.v1.Store;
using API_Shop_Online.Models;
using AutoMapper;

namespace API_Shop_Online.Services.Store
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _bd;
        private readonly IMapper _mapper;

        public StoreService(ApplicationDbContext bd, IMapper mapper)
        {
            _bd = bd;
            _mapper = mapper;
        }

        public ICollection<API_Shop_Online.Models.Store> Get()
        {
            return _bd.Stores.OrderBy(c => c.Name).ToList();
        }

        public API_Shop_Online.Models.Store GetById(int id)
        {
            return _bd.Stores.FirstOrDefault(c => c.Id == id);
        }

        public async Task<API_Shop_Online.Models.Store> Create(StoreSubmissionDto request)
        {
            var entry = _mapper.Map<API_Shop_Online.Models.Store>(request);

            entry.CreatedAt = DateTime.UtcNow;

            _bd.Stores.Add(entry);

            await _bd.SaveChangesAsync();

            var result = _mapper.Map<API_Shop_Online.Models.Store>(entry);
            return result;
        }

        public async Task<API_Shop_Online.Models.Store> Update(int id, StoreSubmissionDto request)
        {
            var entry = GetById(id)!;

            entry.Name = request.Name;
            entry.Address = request.Address;

            _bd.Stores.Update(entry);

            await _bd.SaveChangesAsync();

            var result = _mapper.Map<API_Shop_Online.Models.Store>(entry);

            return result;
        }

        public void Delete(int id)
        {
            var entry = GetById(id);

            _bd.Stores.Remove(entry);

            _bd.SaveChanges();
        }
    }
}
