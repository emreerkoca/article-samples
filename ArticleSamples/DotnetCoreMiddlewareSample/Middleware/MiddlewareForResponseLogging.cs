using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System.IO;
using System.Threading.Tasks;

namespace DotnetCoreMiddlewareSample.Middleware
{
    public class MiddlewareForResponseLogging
    {
        private readonly RequestDelegate _next;
        private ILogger<MiddlewareForResponseLogging> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public MiddlewareForResponseLogging(RequestDelegate next, ILogger<MiddlewareForResponseLogging> logger)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBody = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            
            context.Response.Body = responseBody;
      
            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation($"Response body: {bodyText}");

            await responseBody.CopyToAsync(originalBody);
        }
    }
}
