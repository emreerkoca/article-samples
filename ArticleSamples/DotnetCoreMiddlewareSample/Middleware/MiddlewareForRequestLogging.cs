using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreMiddlewareSample.Middleware
{
    public class MiddlewareForRequestLogging
    {
        private readonly RequestDelegate _next;
        private ILogger<MiddlewareForRequestLogging> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;


        public MiddlewareForRequestLogging(RequestDelegate next, ILogger<MiddlewareForRequestLogging> logger)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestStream = _recyclableMemoryStreamManager.GetStream();

            await context.Request.Body.CopyToAsync(requestStream);

            _logger.LogInformation(
                $@"Request Method: {context.Request?.Method} {Environment.NewLine}
                  Request Path: {context.Request?.Path} {Environment.NewLine}
                  Request Body: {GetBodyFromStream(requestStream)}");

            await _next(context);
        }


        private string GetBodyFromStream(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var body = reader.ReadToEnd();

                stream.Seek(0, SeekOrigin.Begin);

                body = reader.ReadToEnd();

                return body;
            }
        }
    }
}