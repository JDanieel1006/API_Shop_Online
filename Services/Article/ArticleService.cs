using API_Shop_Online.Data;
using API_Shop_Online.Dto.v1.Article;
using AutoMapper;

namespace API_Shop_Online.Services.Article
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _bd;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public ArticleService(ApplicationDbContext bd, IMapper mapper, IWebHostEnvironment env)
        {
            _bd = bd;
            _mapper = mapper;
            _env = env;
        }

        public ICollection<API_Shop_Online.Models.Article> Get()
        {
            return _bd.Articles.OrderBy(c => c.Code).ToList();
        }

        public API_Shop_Online.Models.Article GetById(int id)
        {
            return _bd.Articles.FirstOrDefault(c => c.Id == id);
        }

        public async Task<API_Shop_Online.Models.Article> Create(ArticleSubmissionDto request)
        {
            string uniqueFileName = null;
            if (request.Image != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(fileStream);
                }
            }

            var entry = _mapper.Map<API_Shop_Online.Models.Article>(request);
            entry.Image = uniqueFileName; 

            _bd.Articles.Add(entry);
            await _bd.SaveChangesAsync();

            var result = _mapper.Map<API_Shop_Online.Models.Article>(entry);

            return result;
        }

        public async Task<API_Shop_Online.Models.Article> Update(int id, ArticleSubmissionDto request)
        {
            var entry = GetById(id)!;

            entry.Code = request.Code;
            entry.Description = request.Description;
            entry.Price = request.Price;
            entry.Stock = request.Stock;

            if (request.Image != null && request.Image.Length > 0)
            {
                if (!string.IsNullOrEmpty(entry.Image))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, "images", entry.Image);
                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(fileStream);
                }

                entry.Image = uniqueFileName;
            }

            _bd.Articles.Update(entry);
            await _bd.SaveChangesAsync();

            var result = _mapper.Map<API_Shop_Online.Models.Article>(entry);

            return result;
        }

        public void Delete(int id)
        {
            var entry = GetById(id);

            _bd.Articles.Remove(entry);

            _bd.SaveChanges();
        }
    }
}
