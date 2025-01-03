using FluentValidation;
using LanguageExt;

namespace ShopManager.Features.Shops.Categories.Filters;

public class FilterCreateCategory(IValidator<CreateCategoryDto> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var createCategoryDto = context.GetArgument<CreateCategoryDto>(0);
        var validationResult = await validator.ValidateAsync(createCategoryDto);

        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        return await next(context);
    }
}