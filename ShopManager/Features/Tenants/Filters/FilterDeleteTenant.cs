using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ShopManager.Features.Tenants.Filters;

public class FilterDeleteTenant : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var tenantId = context.GetArgument<Guid>(0);

        return tenantId != Guid.Empty
            ? await next(context)
            : Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "Error", ["TenantId cannot be empty."] }
            });
    }
}