using FluentValidation;
using LanguageExt.Common;
using ShopManager.Features.Tenants.Dtos;
using ShopManager.Features.Tenants.Validations;

namespace ShopManager.Features.Tenants.Filters;

public class FilterDeleteSubscriptionPlanType(IValidator<DeleteSubscriptionPlanTypeDto> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<DeleteSubscriptionPlanTypeDto>(0);
        var validationResult = await validator.ValidateAsync(model);
        
        return validationResult.IsValid ? await next(context) : 
            new Result<IResult>(TypedResults.BadRequest(validationResult.ToDictionary()));
    }
}