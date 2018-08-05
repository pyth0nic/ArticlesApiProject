using System.Collections.Generic;

namespace Articles.Db.Models.ApiModels
{
    public class TagsDto
    {
        public string Tag { get; set; }
        public int Count { get; set; }
        public long[] Articles { get; set; }
        public string[] RelatedTags { get; set; }
    }
}