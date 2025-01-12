using FluentValidation;

namespace ShopManager.Features.Shops.Categories.Filters;

public class FilterDeleteCategory : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var shopId = context.GetArgument<Guid>(0);
        var categoryId = context.GetArgument<Guid>(1);
        if (shopId == Guid.Empty || categoryId == Guid.Empty)
        {
            return TypedResults.BadRequest("ShopId and CategoryId cannot be empty!");
        }
        return await next(context);
    }
}