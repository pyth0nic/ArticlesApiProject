using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

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

        [JsonIgnore]
        [Column("Tags")]
        public string TagList { get; set; }
    }
}