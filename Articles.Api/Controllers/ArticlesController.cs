using System;
using System.Threading.Tasks;
using Articles.Db;
using Articles.Db.Models;
using Articles.Db.Models.ApiModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Articles.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticlesRepository _articlesRepository;
        private readonly ArticlesContext _context;

        public ArticlesController(ArticlesRepository articlesRepository, ArticlesContext context)
        {
            _articlesRepository = articlesRepository;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Articles(ArticleDto article)
        {
            if (article.Title == null || article.Body == null || article.Tags == null)
            {
                return BadRequest("Invalid article");
            }

            var newArticle = await _articlesRepository.AddArticle(article);
            HttpContext.Response.Headers["Location"] = HttpContext.Request.GetDisplayUrl() + "/" + newArticle.Id;

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticleById(int id)
        {
            var article = await _articlesRepository.GetArticle(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }
    }
}