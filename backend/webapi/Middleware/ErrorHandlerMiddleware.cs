using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Webapi.Core.Response;

namespace Webapi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                var result = JsonSerializer.Serialize(new Response<string>(error?.Message), new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                await response.WriteAsync(result);
            }
        }
    }
}
