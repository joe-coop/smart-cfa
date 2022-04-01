using System.Globalization;
using System.Security.Principal;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;

namespace Smart.FA.Catalog.AccountSimulation;

public class ProxyHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public ProxyHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request.GetEncodedUrl();
        if (request.Contains("admin"))
        {
            context.Request.Query.TryGetValue("id", out var idString);

            var partialRequest = (request.Split("/")).Last();

            context.ProxyRedirect('/' + partialRequest, context.Request.Cookies["user-id"]);
        }

        await _next(context);
    }
}

public static class ProxyHeaderMiddlewareExtension
{
    public static IApplicationBuilder UseProxyHeaders(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ProxyHeaderMiddleware>();
    }
}
