using FluentValidation;

namespace ShopManager.Features.Shops.Products.Filters;

public class FilterCreateProduct(IValidator<AddProductDto> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var dto = context.GetArgument<AddProductDto>(1);
        var validationResult = await validator.ValidateAsync(dto).ConfigureAwait(false);
        if (validationResult.IsValid)
        {
            return await next(context);
        }
        return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }
}