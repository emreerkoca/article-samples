using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IDistributedCache _distributedCache;

        List<Article> articleListFromDatabase = new List<Article>()
        {
            new Article { Id = 1, Name = "Article 1", Content = "Sample Content 1" },
            new Article { Id = 2, Name = "Article 2", Content = "Sample Content 2" },
        };

        public ArticleController(ILogger<ArticleController> logger, IDistributedCache distributedCache)
        {
            _logger = logger;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cacheKey = "article-list";
            List<Article> articleList = null;

            var articleListFromCache = await _distributedCache.GetAsync(cacheKey);

            if (articleListFromCache != null)
            {
                articleList = JsonConvert.DeserializeObject<List<Article>>(Encoding.UTF8.GetString(articleListFromCache));

                _logger.LogInformation("Article List object is getting from cache.");
            }
            else
            {
                articleList = articleListFromDatabase;

                var serializedArticleList = JsonConvert.SerializeObject(articleList);
                var encodedArticleList = Encoding.UTF8.GetBytes(serializedArticleList);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                await _distributedCache.SetAsync(cacheKey, encodedArticleList, options);

                _logger.LogInformation("Article List object is setting to cache.");
            }

            return StatusCode((int)HttpStatusCode.OK, articleList);
        }
    }
}
