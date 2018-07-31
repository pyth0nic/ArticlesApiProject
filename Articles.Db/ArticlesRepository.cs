using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Articles.Db.Models;
using Articles.Db.Models.ApiModels;
using Microsoft.EntityFrameworkCore;

namespace Articles.Db
{
    public class ArticlesRepository
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
                Tags = tags
            });
            await _context.SaveChangesAsync(true);
            return newArticle.Entity;
        }

        public async Task<Article> GetArticle(int id)
        {
            return await _context.Articles.FirstOrDefaultAsync(x => x.Id == id);;
        }

        private async Task<List<Tag>> UpsertTags(IEnumerable<string> tags)
        {
            var tagList = new List<Tag>();
            foreach (var tag in tags)
            {
                tagList.Add(await UpsertTag(tag,false));
            }

            return tagList;
        }
        
        // No update at this point
        private async Task<Tag> UpsertTag(string tag, bool saveNow=true)
        {
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Name == NormaliseTag(tag));

            if (existingTag != null)
            {
                return existingTag;
            }
            
            var newTag = (await _context.Tags.AddAsync(new Tag {Name = NormaliseTag(tag)})).Entity;
            
            if (saveNow) 
                await _context.SaveChangesAsync(true);
            return newTag;
        }

        // todo move to utils
        private string NormaliseTag(string tag)
        {
            TextInfo textInfo = new CultureInfo("en-AU",false).TextInfo;
            return textInfo.ToTitleCase(tag);
        }
        
    }
}