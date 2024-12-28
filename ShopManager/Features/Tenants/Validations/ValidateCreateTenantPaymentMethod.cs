using FluentValidation;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.Validations;

public class ValidateCreateTenantPaymentMethod : AbstractValidator<CreateTenantPaymentMethodDto>
{
    public ValidateCreateTenantPaymentMethod()
    {
        RuleFor(x => x.TenantId).NotEmpty();
        RuleFor(x => x.PaymentDetails)
            .NotEmpty();
    }
}