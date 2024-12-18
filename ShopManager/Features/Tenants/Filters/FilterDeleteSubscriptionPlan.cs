using FluentValidation;
using LanguageExt.Common;
using ShopManager.Features.Tenants.Validations;

namespace ShopManager.Features.Tenants.Filters;

public class FilterDeleteSubscriptionPlan(IValidator<DeleteSubscriptionPlanDto> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var subscriptionPlanId = context.GetArgument<Guid>(0);
        var validationResult = await validator.ValidateAsync(new DeleteSubscriptionPlanDto(subscriptionPlanId));
        return validationResult.IsValid ? await next(context) : new Result<IResult>(
            TypedResults.ValidationProblem(validationResult.ToDictionary()));
    }
}