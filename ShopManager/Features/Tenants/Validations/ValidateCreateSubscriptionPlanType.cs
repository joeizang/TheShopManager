using FluentValidation;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.Validations;

public class ValidateCreateSubscriptionPlanType : AbstractValidator<CreateSubscriptionPlanTypeDto>
{
    public ValidateCreateSubscriptionPlanType()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Features).NotEmpty().WithMessage("Features is required");
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount must be greater than or equal to 0");
    }
}