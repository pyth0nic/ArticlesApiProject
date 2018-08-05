using System;
using Microsoft.EntityFrameworkCore;

namespace Articles.Db.Tests
{
    public static class ContextUtil
    {
        public static ArticlesContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ArticlesContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ArticlesContext(options);
        }
    }
}