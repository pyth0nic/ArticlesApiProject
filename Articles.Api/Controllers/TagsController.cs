using System;
using System.Threading.Tasks;
using Articles.Db.Models.ApiModels;
using Articles.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Articles.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : Controller
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly ILogger<TagsController> _log;

        public TagsController(ITagsRepository tagsRepository, ILogger<TagsController> log)
        {
            _tagsRepository = tagsRepository;
            _log = log;
        }

        [HttpGet("{tag}/{date}")]
        public async Task<ActionResult<TagsDto>> GetArticleById(string tag, string date)
        {
            var formattedDate = DateTime.ParseExact(date, "yyyyMMdd", null);
            _log.LogInformation($"Good or bad request: tag - {tag} date - {date}");

            if (formattedDate == default(DateTime))
            {
                _log.LogInformation($"Bad request: tag - {tag} date - {date}");
                return BadRequest();
            }
    
            try
            {
                var result = await _tagsRepository.GetTagsByTagNameAndDate(tag, formattedDate);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, $"Error in GetArticleById: tag - {tag} date - {date}");
                return BadRequest();
            } 
        }
    }
}