using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
#nullable disable
namespace Cafe.API.Middleware
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "An internal server error occurred.",
                    Error = ex.Message
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = errorResponse.Status;

                var result = JsonConvert.SerializeObject(errorResponse);
                await context.Response.WriteAsync(result);
            }
        }


    }

    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);

    }
}