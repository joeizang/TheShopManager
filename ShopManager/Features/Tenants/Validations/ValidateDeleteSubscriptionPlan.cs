using FluentValidation;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.Validations;

public class ValidateDeleteSubscriptionPlan : AbstractValidator<DeleteSubscriptionPlanDto>
{
    public ValidateDeleteSubscriptionPlan()
    {
        RuleFor(x => x.SubscriptionPlanId).NotEmpty();
    }
}