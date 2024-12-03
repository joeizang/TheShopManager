using FluentValidation;

namespace ShopManager.Features.Tenants.Filters;

public class FilterDeleteTenant(IValidator<DeleteTenantDto> validator) : IEndpointFilter
{
    private readonly IValidator<DeleteTenantDto> _validator = validator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var tenantId = context.GetArgument<DeleteTenantDto>(1);
        var validationResult = await _validator.ValidateAsync(tenantId);
        
        return validationResult.IsValid ? await next(context)
            : Results.ValidationProblem(validationResult.ToDictionary());
    }
}