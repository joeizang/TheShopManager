using NodaTime;
using ShopManager.Core;

namespace ShopManager.Features.Shops.Products.Filters;

public class FilterGetProducts : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var cursorString = context.GetArgument<string>(1);
        var cursorCheck = cursorString.TryParseStringToInstant(out _);
        if (!cursorCheck)
        {
            return Results.BadRequest("Invalid cursor");
        }

        return await next(context);
    }
}