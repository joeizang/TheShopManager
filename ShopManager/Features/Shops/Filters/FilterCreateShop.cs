using FluentValidation;

namespace ShopManager.Features.Shops.Filters;

public class FilterCreateShop(IValidator<CreateShopDto> validator) : IEndpointFilter
{
    private readonly IValidator<CreateShopDto> _validator = validator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<CreateShopDto>(1);
        var validationResult = await _validator.ValidateAsync(model);
        if (validationResult.IsValid)
        {
            return await next(context);
        }

        return Results.ValidationProblem(validationResult.ToDictionary());
    }
}