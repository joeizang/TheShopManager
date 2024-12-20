using FluentValidation;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.Validations;

public class ValidateDeleteSubscriptionPlanType : AbstractValidator<DeleteSubscriptionPlanTypeDto>
{
    public ValidateDeleteSubscriptionPlanType()
    {
        RuleFor(x => x.SubscriptionPlanTypeId).NotEmpty();
    }
}