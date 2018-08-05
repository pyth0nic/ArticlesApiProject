using System;
using System.Threading.Tasks;
using Articles.Db.Models.ApiModels;
using Articles.Db.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : Controller
    {
        private readonly TagsRepository _tagsRepository;

        public TagsController(TagsRepository tagsRepository)
        {
            _tagsRepository = tagsRepository;
        }

        [HttpGet("{tag}/{date}")]
        public async Task<ActionResult<TagsDto>> GetArticleById(string tag, string date)
        {
            var formattedDate = DateTime.ParseExact(date, "yyyyMMdd", null);
            if (formattedDate == default(DateTime))
            {
                return BadRequest();
            }

            var result = await _tagsRepository.GetTagsByTagNameAndDate(tag, formattedDate);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}