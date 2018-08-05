using System;
using System.Linq;
using System.Threading.Tasks;
using Articles.Db.Models.ApiModels;
using Articles.Db.Repositories.Util;

namespace Articles.Db.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly ArticlesContext _articlesContext;

        public TagsRepository(ArticlesContext articlesContext)
        {
            _articlesContext = articlesContext;
        }

        public async Task<TagsDto> GetTagsByTagNameAndDate(string tag, DateTime date)
        {
            if (tag == null || date == default(DateTime))
            {
                return null;
            }

            var normalisedTag = TagNormaliser.Normalise(tag);

            var articles = _articlesContext.Articles
                .Where(x => x.Tags.Any(y => y == normalisedTag) && x.Date.Date == date.Date).ToList();
            
            if (!articles.Any())
            {
                return null;
            }

            // rather big linq statement -> select tags lists from each article and flatten
            var relatedTags = articles
                .SelectMany(x => x.Tags)
                // Remove present tag
                .Where(x=> x != normalisedTag)
                // deduplicate
                .ToHashSet();

            var tagDto = new TagsDto
            {
                Tag = normalisedTag,
                // take a maximum of 10 articles
                Articles = articles.Select(x => x.Id).Take(10).ToArray(),
                // Get total related tags for the day
                Count = articles.SelectMany(x=> x.Tags).Count(),
                RelatedTags = relatedTags.ToArray()
            };
            return tagDto;
        }
    }
}