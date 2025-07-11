using API_Shop_Online.Data;
using API_Shop_Online.Dto.v1.Store;
using API_Shop_Online.Models;
using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace API_Shop_Online.Services.Store
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _bd;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public StoreService(ApplicationDbContext bd, IMapper mapper, IWebHostEnvironment env)
        {
            _bd = bd;
            _mapper = mapper;
            _env = env;
        }

        public ICollection<API_Shop_Online.Models.Store> Get()
        {
            return _bd.Stores.OrderBy(c => c.Name).ToList();
        }

        public API_Shop_Online.Models.Store GetById(int id)
        {
            return _bd.Stores.FirstOrDefault(c => c.Id == id);
        }

        public async Task<StoreArticleDto?> GetStoreArticle(int storeId, int articleId)
        {
            var entity = await _bd.StoreArticles
                .FirstOrDefaultAsync(x => x.StoreId == storeId && x.ArticleId == articleId);

            return entity == null ? null : _mapper.Map<StoreArticleDto>(entity);
        }

        public async Task<List<StoreArticleDto>> GetArticlesByStore(int storeId, string baseUrl)
        {
            var list = await (from sa in _bd.StoreArticles
                              join s in _bd.Stores on sa.StoreId equals s.Id
                              join a in _bd.Articles on sa.ArticleId equals a.Id
                              where sa.StoreId == storeId
                              select new StoreArticleDto
                              {
                                  StoreId = sa.StoreId,
                                  StoreName = s.Name,
                                  ArticleId = a.Id,
                                  ArticleCode = a.Code,
                                  ArticleDescription = a.Description,
                                  ArticlePrice = a.Price,
                                  ImageUrl = !string.IsNullOrEmpty(a.Image) ? baseUrl + a.Image : null,
                                  CreatedAt = sa.CreatedAt
                              }).ToListAsync();

            return list;
        }

        public async Task<API_Shop_Online.Models.Store> Create(StoreSubmissionDto request)
        {

            string uniqueFileName = null;
            if (request.Image != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "stores");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(fileStream);
                }
            }

            var entry = _mapper.Map<API_Shop_Online.Models.Store>(request);

            entry.CreatedAt = DateTime.UtcNow;

            _bd.Stores.Add(entry);

            await _bd.SaveChangesAsync();

            var result = _mapper.Map<API_Shop_Online.Models.Store>(entry);
            return result;
        }

        public async Task<StoreArticleDto> AddArticleToStore(int storeId,StoreArticleSubmissionDto dto)
        {
            var existingRelations = await _bd.StoreArticles
                                            .Where(sa => sa.StoreId == storeId)
                                            .ToListAsync();

            _bd.StoreArticles.RemoveRange(existingRelations);

            var storeArticle = new StoreArticle
            {
                StoreId = storeId,
                ArticleId = dto.ArticleId,
                CreatedAt = DateTime.UtcNow
            };

            _bd.StoreArticles.Add(storeArticle);
            await _bd.SaveChangesAsync();

            return _mapper.Map<StoreArticleDto>(storeArticle);
        }

        public async Task<API_Shop_Online.Models.Store> Update(int id, StoreSubmissionDto request)
        {
            var entry = GetById(id)!;

            entry.Name = request.Name;
            entry.Address = request.Address;

            if (request.Image != null && request.Image.Length > 0)
            {
                if (!string.IsNullOrEmpty(entry.Image))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, "stores", entry.Image);
                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
                string uploadsFolder = Path.Combine(_env.WebRootPath, "stores");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(fileStream);
                }

                entry.Image = uniqueFileName;
            }

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
