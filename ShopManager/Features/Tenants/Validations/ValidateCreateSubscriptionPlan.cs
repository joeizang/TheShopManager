using FluentValidation;

namespace ShopManager.Features.Tenants.Validations;

public class ValidateCreateSubscriptionPlan : AbstractValidator<CreateSubscriptionPlanDto>
{
    public ValidateCreateSubscriptionPlan()
    {

        RuleFor(x => x.TenantId)
            .NotEmpty()
            .WithMessage("TenantId is required");

        RuleFor(x => x.SubscriptionPlanTypeId)
            .NotEmpty()
            .WithMessage("SubscriptionPlanTypeId is required");
    }
}