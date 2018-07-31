using Articles.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Articles.Db
{
    public class ArticlesContext: DbContext
    {
        public ArticlesContext(DbContextOptions<ArticlesContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}