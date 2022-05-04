using BlogApp.Core.Utilities.Exceptions;
using BlogApp.Core.Utilities.Results.Concrete;
using System.Net;
using System.Text.Json;

namespace BlogApp.API.Middlewares;

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

            var responseModel = new ErrorResult(error.Message);

            response.StatusCode = error switch
            {
                AppException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}
