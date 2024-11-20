using FluentValidation;
using ShopManager.Features.Shops.Validations;

namespace ShopManager.Features.Shops.Filters;

public class FilterDeleteShop(IValidator<DeleteShopDto> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var payload = context.GetArgument<DeleteShopDto>(1);
        var validationResult = await validator.ValidateAsync(payload);
        return validationResult.IsValid ? await next(context)
            : Results.ValidationProblem(validationResult.ToDictionary());
    }
}