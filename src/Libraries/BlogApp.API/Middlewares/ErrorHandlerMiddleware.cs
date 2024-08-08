using BlogApp.Core.Utilities.Exceptions;
using BlogApp.Core.Utilities.Results.Concrete;
using System.Text.Json;

namespace BlogApp.API.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var responseModel = Result.Failure(new("500", error.Message));

            response.StatusCode = error switch
            {
                DatabaseValidationException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}