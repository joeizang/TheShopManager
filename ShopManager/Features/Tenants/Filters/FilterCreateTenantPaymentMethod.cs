using FluentValidation;
using LanguageExt.Common;
using ShopManager.Features.Tenants.Dtos;
using ShopManager.Features.Tenants.Validations;

namespace ShopManager.Features.Tenants.Filters;

public class FilterCreateTenantPaymentMethod(IValidator<CreateTenantPaymentMethodDto> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var payload = context.GetArgument<CreateTenantPaymentMethodDto>(0);
        var validationResult = await validator.ValidateAsync(payload);
        return validationResult.IsValid ? await next(context) 
            : new Result<IResult>(Results.ValidationProblem(validationResult.ToDictionary()));
    }
}