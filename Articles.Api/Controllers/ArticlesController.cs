using Articles.Db;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticlesContext _articlesContext;

        public ArticlesController(ArticlesContext articlesContext)
        {
            _articlesContext = articlesContext;
        }

        [HttpGet]
        public ActionResult<string> HelloWorld()
        {
            return "Hello World!";
        }
    }
}