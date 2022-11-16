using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BlogApp.MVCUI.Filters;

public class CustomExceptionFilter : IAsyncExceptionFilter
{
    private readonly ILogger _logger;

    public CustomExceptionFilter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("Exception");
    }

    public Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is HttpRequestException requestException && requestException.StatusCode == HttpStatusCode.Unauthorized)
        {
            context.Result = new RedirectToActionResult("Login", "Home", null);
            context.HttpContext.Session.Clear();
            context.HttpContext.SignOutAsync();

            _logger.LogError(context.Exception.ToString());
            context.ExceptionHandled = true;
        }

        return Task.CompletedTask;
    }

}
