using FluentValidation;

namespace ShopManager.Features.Tenants.Validations;

public class ValidateDeleteSubscriptionPlan : AbstractValidator<DeleteSubscriptionPlanDto>
{
    public ValidateDeleteSubscriptionPlan()
    {
        RuleFor(x => x.SubscriptionPlanId).NotEmpty();
    }
}