using FluentValidation;
using ShopManager.Features.Shops.Validations;

namespace ShopManager.Features.Shops.Filters;

public class FilterUpdateShop(IValidator<UpdateShopDto> validator) : IEndpointFilter
{
    private readonly IValidator<UpdateShopDto> _validator = validator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<UpdateShopDto>(1);
        var validationResult = await _validator.ValidateAsync(model);

        if (validationResult.IsValid)
        {
            return await next(context);
        }

        return Results.ValidationProblem(validationResult.ToDictionary());
    }
}