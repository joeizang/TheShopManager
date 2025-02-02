using System;

namespace ShopManager.Core.Middleware;

public class ShopIdentityResolverMiddleware
{
    private readonly RequestDelegate _next;

    public ShopIdentityResolverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IShopManagerContextAccessor shopManagerContextAccessor)
    {
        if (context.Request.Headers.TryGetValue("X-Shop-Id", out var shopIdHeader) &&
        Guid.TryParse(shopIdHeader, out var result))
        {
            shopManagerContextAccessor.SetShopId(result);
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("X-Shop-Id header is required");
            return;
        }
        await _next(context);
    }
}
