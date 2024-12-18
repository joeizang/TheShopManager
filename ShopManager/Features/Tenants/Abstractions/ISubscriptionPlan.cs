using LanguageExt.Common;
using ShopManager.Features.Tenants.DomainModels;

namespace ShopManager.Features.Tenants.Abstractions;

public interface ISubscriptionPlan
{
    Task<Result<SubscriptionPlanTypeDto>> CreateSubscriptionPlanType(CreateSubscriptionPlanTypeDto inputModel);
    
    Task<Result<SubscriptionPlanDto>> CreateSubscriptionPlan(CreateSubscriptionPlanDto inputModel);
    
    Task<Result<SubscriptionPlanDto>> UpdateSubscriptionPlan(UpdateSubscriptionPlanDto inputModel);
    
    Task<Result<IResult>> DeleteSubscriptionPlan(Guid subscriptionPlanId);
    
    Task<Result<IResult>> DeleteSubscriptionPlanType(Guid subscriptionPlanTypeId);
}