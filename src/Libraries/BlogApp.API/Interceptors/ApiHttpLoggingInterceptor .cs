using Microsoft.AspNetCore.HttpLogging;

namespace BlogApp.API.Interceptors;
public class ApiHttpLoggingInterceptor : IHttpLoggingInterceptor
{
    public ValueTask OnRequestAsync(HttpLoggingInterceptorContext logContext)
    {
        if (logContext.TryDisable(HttpLoggingFields.RequestPath))
        {
            RedactPath(logContext);
        }

        if (logContext.TryDisable(HttpLoggingFields.RequestHeaders))
        {
            RedactRequestHeaders(logContext);
        }

        EnrichRequest(logContext);

        return default;
    }

    public ValueTask OnResponseAsync(HttpLoggingInterceptorContext logContext)
    {
        if (logContext.TryDisable(HttpLoggingFields.ResponseHeaders))
        {
            RedactResponseHeaders(logContext);
        }

        EnrichResponse(logContext);

        return default;
    }

    private static void RedactPath(HttpLoggingInterceptorContext logContext) => logContext.AddParameter(nameof(logContext.HttpContext.Request.Path), "RedactedPath");

    private static void RedactRequestHeaders(HttpLoggingInterceptorContext logContext)
    {
        foreach (var header in logContext.HttpContext.Request.Headers)
        {
            logContext.AddParameter(header.Key, "RedactedHeader");
        }
    }

    private static void EnrichRequest(HttpLoggingInterceptorContext logContext) => logContext.AddParameter("RequestEnrichment", "Stuff");

    private static void RedactResponseHeaders(HttpLoggingInterceptorContext logContext)
    {
        foreach (var header in logContext.HttpContext.Response.Headers)
        {
            logContext.AddParameter(header.Key, "RedactedHeader");
        }
    }

    private static void EnrichResponse(HttpLoggingInterceptorContext logContext) => logContext.AddParameter("ResponseEnrichment", "Stuff");
}
