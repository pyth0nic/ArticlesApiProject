using System;
using System.Collections.Generic;
using System.Linq;
using Articles.Db.Models;
using Articles.Db.Models.ApiModels;
using Articles.Db.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Articles.Db.Tests
{
    [TestClass]
    public class ArticlesRepositoryTests
    {        
        [TestMethod]
        public void AddArticle_AddsArticle_WhenValidData()
        {
            using (var ctx = ContextUtil.GetContext())
            {
                var repo = new ArticlesRepository(ctx);
                
                var article = new ArticleDto
                {
                    Title = "The empire actually strikes back!?",
                    Body = "[Insert something funny here]",
                    Tags = new string[] { "star wars", "light side", "dark side", "dark side" }
                };
                
                var result = repo.AddArticle(article).GetAwaiter().GetResult();
                
                Assert.AreEqual("The empire actually strikes back!?", result.Title);
                Assert.AreEqual("[Insert something funny here]", result.Body);

                new List<string>()
                {
                    "Star Wars",
                    "Light Side",
                    "Dark Side"
                }.Select(x=>
                {
                    var tag = result.Tags.First(y => x == y);
                    Assert.AreEqual(x, tag);
                    return tag;
                });

                // Give it two minutes
                Assert.IsTrue(result.Date > DateTime.UtcNow.AddMinutes(-1) && result.Date < DateTime.UtcNow.AddMinutes(1));
            }
        }

        [TestMethod]
        public void GetArticle_GetsArticle_WhenExists()
        {
            using (var ctx = ContextUtil.GetContext())
            {
                var tags = new List<string>()
                {
                    "Headlines",
                    "Morning"
                };
                var article = new Article
                {
                    Id = 1,
                    Title = "Good Morning!",
                    Body = "Morning headlines:",
                    Tags = tags.ToArray()
                };
                ctx.Articles.Add(article);
                ctx.SaveChanges(true);
                
                var repo = new ArticlesRepository(ctx);
                var result = repo.GetArticle(1).GetAwaiter().GetResult();
                Assert.AreEqual(article, result);
            }
        }
        
        [TestMethod]
        public void GetArticle_ReturnsNull_WhenArticleDoesntExist()
        {
            using (var ctx = ContextUtil.GetContext())
            {
                var repo = new ArticlesRepository(ctx);
                var result = repo.GetArticle(1).GetAwaiter().GetResult();
                Assert.IsNull(result);
            }
        }
    }
}
