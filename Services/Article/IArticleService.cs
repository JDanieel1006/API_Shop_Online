using API_Shop_Online.Dto.v1.Article;

namespace API_Shop_Online.Services.Article
{
    public interface IArticleService
    {
        ICollection<API_Shop_Online.Models.Article> Get();

        API_Shop_Online.Models.Article? GetById(int id);

        Task<API_Shop_Online.Models.Article> Create(ArticleSubmissionDto request);

        Task<API_Shop_Online.Models.Article> Update(int id, ArticleSubmissionDto request);

        void Delete(int id);
    }
}
