using System.Threading.Tasks;
using Articles.Db.Models;
using Articles.Db.Models.ApiModels;

namespace Articles.Db.Repositories
{
    public interface IArticlesRepository
    {
        Task<Article> AddArticle(ArticleDto articleDto);
        Task<Article> GetArticle(int id);
    }
}