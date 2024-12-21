using FluentValidation;
using LanguageExt.Common;
using ShopManager.Features.Tenants.Dtos;
using ShopManager.Features.Tenants.Validations;

namespace ShopManager.Features.Tenants.Filters;

public class FilterCreateSubscriptionPlan(IValidator<CreateSubscriptionPlanDto> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.GetArgument<CreateSubscriptionPlanDto>(1);
        var validationResult = await validator.ValidateAsync(model);
        return validationResult.IsValid ? await next(context)
            : new Result<IResult>(Results.ValidationProblem(validationResult.ToDictionary()));
    }
}