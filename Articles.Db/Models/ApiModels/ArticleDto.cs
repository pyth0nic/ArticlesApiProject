using System.Collections.Generic;
using System.Linq;

namespace Articles.Db.Models.ApiModels
{
    public class ArticleDto
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Tags { get; set; }
        
            
        public static ArticleDto FromArticle(Article article)
        {
            return new ArticleDto
            {
                Title = article.Title,
                Body = article.Body,
                Date = article.Date.ToString("yyyy-mm-dd"),
                Tags = article.Tags.Select(x => x.Name)
            };
        }
    }
}