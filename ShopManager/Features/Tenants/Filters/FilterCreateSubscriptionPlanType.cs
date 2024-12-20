using FluentValidation;
using LanguageExt.Common;

namespace ShopManager.Features.Tenants.Filters;

public class FilterCreateSubscriptionPlanType(IValidator<CreateSubscriptionPlanTypeDto> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<CreateSubscriptionPlanTypeDto>(0);
        var validationResult = await validator.ValidateAsync(model);
        
        return validationResult.IsValid ? await next(context) : 
            new Result<IResult>(TypedResults.BadRequest(validationResult.ToDictionary()));
    }
}