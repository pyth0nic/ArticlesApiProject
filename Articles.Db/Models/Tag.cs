using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Articles.Db.Models
{
    public class Tag
    {
        [Key]
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        //public List<Article> Articles { get; set; }
    }
}