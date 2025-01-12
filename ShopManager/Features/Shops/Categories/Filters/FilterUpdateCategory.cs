using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;

namespace ShopManager.Features.Shops.Categories.Filters;

public class FilterUpdateCategory(IValidator<CreateCategoryDto> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var categoryId = context.GetArgument<Guid>(0);
        var dto = context.GetArgument<CreateCategoryDto>(1);
        var validationResult = await validator.ValidateAsync(dto).ConfigureAwait(false);
        if (validationResult.IsValid && categoryId != Guid.Empty) return await next(context);
        validationResult.Errors.Add(new ValidationFailure
        {
            PropertyName = "categoryId",
            ErrorMessage = "Category Id cannot be empty!"
        });
        return new Result<IResult>(TypedResults.BadRequest(validationResult.ToDictionary()));
    }
}