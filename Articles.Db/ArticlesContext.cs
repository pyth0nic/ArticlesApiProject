using Articles.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.Db
{
    public class ArticlesContext: DbContext
    {
        public ArticlesContext(DbContextOptions<ArticlesContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Article> Articles { get; set; }
    }
}