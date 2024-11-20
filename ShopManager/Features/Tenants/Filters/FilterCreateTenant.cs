
using FluentValidation;

namespace ShopManager.Features.Tenants.Filters
{
    public class FilterCreateTenant(IValidator<CreateTenantDto> validator) : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var model = context.GetArgument<CreateTenantDto>(1);
            var validationResult = await validator.ValidateAsync(model);
            return validationResult.IsValid ? await next(context)
                : Results.ValidationProblem(validationResult.ToDictionary());
        }
    }
}
