using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Articles.Db.Models;
using Articles.Db.Models.ApiModels;
using Articles.Db.Repositories.Util;
using Microsoft.EntityFrameworkCore;

namespace Articles.Db.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly ArticlesContext _context;

        public ArticlesRepository(ArticlesContext context)
        {
            _context = context;
        }

        public async Task<Article> AddArticle(ArticleDto articleDto)
        {
            var tags = await UpsertTags(articleDto.Tags);
            var newArticle = await _context.Articles.AddAsync(new Article
            {
                Title = articleDto.Title,
                Body = articleDto.Body,
                Date = DateTime.UtcNow,
                Tags = tags.ToArray()
            });
            
            await _context.SaveChangesAsync(true);
            return newArticle.Entity;
        }

        public async Task<Article> GetArticle(int id)
        {
            return await _context.Articles.FirstOrDefaultAsync(x => x.Id == id);;
        }

        private async Task<List<string>> UpsertTags(IEnumerable<string> tags)
        {
            var tagList = new List<string>();
            foreach (var tag in tags)
            {
                var tagEntity = await UpsertTag(tag, tagList);
                if (tagEntity == null)
                    continue;

                tagList.Add(tagEntity);
            }

            return tagList;
        }
        
        // No update at this point
        private async Task<string> UpsertTag(string tag, List<string> tagList)
        {
            if (tagList.Any(x => x == TagNormaliser.Normalise(tag)))
            {
                return null;
            }
            
            return TagNormaliser.Normalise(tag);
        }
    }
}