using System;
using System.Collections.Generic;
using System.Linq;
using Articles.Db.Models;
using Articles.Db.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Articles.Db.Tests
{
    [TestClass]
    public class TagsRepositoryTests
    {
        [TestMethod]
        public void GetTagsByTagNameAndDate_GetsTagDto_WhenArticlesExistForTagAndDate()
        {
            using (var ctx = ContextUtil.GetContext())
            {
                var tags = new string[]
                {
                    "Health", "Science", "Fitness",
                    "Crime", "Cars", "Driving", "Fruitarian", "See Food Diet", "Lifestyle"
                };

                var articles = GetArticleData(tags.ToList());

                foreach (var article in articles)
                {
                    ctx.Articles.Add(article);
                    ctx.SaveChanges(true);
                }

                var repo = new TagsRepository(ctx);
                var result = repo.GetTagsByTagNameAndDate("Health", DateTime.UtcNow).GetAwaiter().GetResult();

                Assert.AreEqual("Health", result.Tag);

                // The count field shows the number of tags for the tag for that day.
                Assert.AreEqual(7, result.Count);

                // count of unique tags related to health
                CollectionAssert.AreEquivalent(
                    new string[] {"Science", "Fitness", "Fruitarian", "See Food Diet"}, result.RelatedTags.ToArray());

                // The articles field contains a list of ids for the last 10 articles entered for that day.
                Assert.AreEqual(3, result.Articles.Length);
            }
        }

        /*
         {
            "tag" : "health",
            "count" : 17,
            "articles" :
              [
                "1",
                "7"
              ],
            "related_tags" :
              [
                "science",
                "fitness"
              ]
         }
         */
        private List<Article> GetArticleData(List<string> tagList)
        {
            var articles = new List<Article>();

            var variousCategories = new List<string[]>
            {
                new[] {tagList[0], tagList[1], tagList[2]},
                new[] {tagList[3]},
                new[] {tagList[4], tagList[5]},
                new[] {tagList[0], tagList[6]},
                new[] {tagList[0], tagList[7]}
            };

            var unrelatedCategories = new List<string[]>
            {
                new[] {tagList[8]}
            };

            // Add todays articles
            BuildArticles(articles, variousCategories, true);
            // Add some unrelate articles
            for (int i = 0; i < 10; i++)
            {
                BuildArticles(articles, unrelatedCategories, true);
            }

            // Add some more articles
            BuildArticles(articles, variousCategories, false);

            return articles;
        }

        private void BuildArticles(List<Article> articles, List<string[]> relatedCategories, bool today)
        {
            var offset = Math.Max(articles.Count, 0) + 1;
            for (var i = 0; i < relatedCategories.Count; i++)
            {
                var article = new Article
                {
                    Id = i + offset,
                    Body = "",
                    Date = today ? DateTime.UtcNow : DateTime.UtcNow.AddDays(-1),
                    Tags = relatedCategories[i].ToArray()
                };

                articles.Add(article);
            }
        }
    }
}