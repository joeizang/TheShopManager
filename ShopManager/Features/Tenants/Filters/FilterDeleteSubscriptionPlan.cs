namespace ShopManager.Features.Tenants.Filters;

public class FilterDeleteSubscriptionPlan : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        return await next(context);
    }
}