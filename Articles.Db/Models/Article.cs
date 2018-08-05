using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Internal;

namespace Articles.Db.Models
{
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }

        // for future performance use postgres's inbuilt array
        [NotMapped]
        public string[] Tags
        {
            get => TagList.Split(",");
            set => TagList = value.Join(",");
        }

        [Column("Tags")]
        public string TagList { get; set; }
    }
}