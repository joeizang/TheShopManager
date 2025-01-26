
using FluentValidation;

namespace ShopManager.Features.Shops.Products.Filters;

public class FilterDeleteProduct : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var shopId = context.GetArgument<Guid>(0);
        var productId = context.GetArgument<Guid>(1);

        if(shopId == Guid.Empty || productId == Guid.Empty)
        {
            return new ValueTask<object?>(TypedResults.BadRequest());
        }

        return await next(context);

    }
}
