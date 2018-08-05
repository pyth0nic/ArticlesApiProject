using System;
using System.Threading.Tasks;
using Articles.Db.Models.ApiModels;

namespace Articles.Db.Repositories
{
    public interface ITagsRepository
    {
        Task<TagsDto> GetTagsByTagNameAndDate(string tag, DateTime date);
    }
}