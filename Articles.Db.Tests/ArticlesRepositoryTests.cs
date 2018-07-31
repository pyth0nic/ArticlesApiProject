using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Articles.Db.Models;
using Articles.Db.Models.ApiModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Articles.Db.Tests
{
    [TestClass]
    public class ArticlesRepositoryTests
    {
        [TestMethod]
        public void Works()
        {
            Assert.IsFalse(false);
            Assert.AreEqual(1,1);
        }
        
        [TestMethod]
        public void AddArticle_AddsArticle_WhenValidData()
        {
            using (var ctx = GetContext())
            {
                var repo = new ArticlesRepository(ctx);
                
                var article = new ArticleDto
                {
                    Title = "The empire actually strikes back!?",
                    Body = "[Insert something funny here]",
                    Tags = new string[] { "star wars", "light side", "dark side" }
                };
                
                var result = repo.AddArticle(article).GetAwaiter().GetResult();
                
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("The empire actually strikes back!?", result.Title);
                Assert.AreEqual("[Insert something funny here]", result.Body);

                new List<Tag>()
                {
                    new Tag {Id = 1, Name = "Star Wars"},
                    new Tag {Id = 2, Name = "Light Side"},
                    new Tag {Id = 3, Name = "Dark Side"}
                }.Select(x=>
                {
                    var tag = result.Tags.First(y => x.Id == y.Id);
                    Assert.AreEqual(x.Id, tag.Id);
                    Assert.AreEqual(x.Id, tag.Name);
                    return tag;
                });
                
                // Give it two minutes
                Assert.IsTrue(result.Date > DateTime.UtcNow.AddMinutes(-1) && result.Date < DateTime.UtcNow.AddMinutes(1));
            }
        }

        private ArticlesContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ArticlesContext>()
                .UseInMemoryDatabase(databaseName: "db")
                .Options;
            return new ArticlesContext(options);
        }
    }
}
