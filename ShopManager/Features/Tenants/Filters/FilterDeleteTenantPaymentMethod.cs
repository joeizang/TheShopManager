using LanguageExt.Common;

namespace ShopManager.Features.Tenants.Filters;

public class FilterDeleteTenantPaymentMethod : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var payload = context.GetArgument<Guid>(0);
        if (payload == Guid.Empty)
            return new Result<IResult>(Results.BadRequest("Id cannot be empty and is required!"));
        return await next(context);
    }
}