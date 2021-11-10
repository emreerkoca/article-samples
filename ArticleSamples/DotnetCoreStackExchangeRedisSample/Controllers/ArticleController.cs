using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreStackExchangeRedisSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {

        private readonly ILogger<ArticleController> _logger;

        List<Article> articleListFromDatabase = new List<Article>()
        {
            new Article { Id = 1, Name = "Article 1", Content = "Sample Content 1" },
            new Article { Id = 2, Name = "Article 2", Content = "Sample Content 2" },
        };

        public ArticleController(ILogger<ArticleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cacheKey = "article-list";
            List<Article> articleList = null;

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase database = redis.GetDatabase();

            var articleListFromCache = await database.StringGetAsync(cacheKey);

            if (articleListFromCache.HasValue)
            {
                articleList = JsonConvert.DeserializeObject<List<Article>>(articleListFromCache);

                _logger.LogInformation("Article List object is getting from cache.");
            }
            else
            {
                articleList = articleListFromDatabase;

                var serializedArticleList = JsonConvert.SerializeObject(articleList);

                await database.StringSetAsync(cacheKey, serializedArticleList);

                _logger.LogInformation("Article List object is setting to cache.");
            }

            return StatusCode((int)HttpStatusCode.OK, articleList);
        }
    }
}
