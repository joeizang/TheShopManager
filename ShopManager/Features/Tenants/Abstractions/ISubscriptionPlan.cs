using ShopManager.Features.Tenants.DomainModels;

namespace ShopManager.Features.Tenants.Abstractions;

public interface ISubscriptionPlan
{
    Task<SubscriptionPlanTypeDto> CreateSubscriptionPlanType(CreateSubscriptionPlanTypeDto inputModel);
    
    Task<SubscriptionPlanDto> CreateSubscriptionPlan(CreateSubscriptionPlanDto inputModel);
    
    Task<SubscriptionPlanDto> UpdateSubscriptionPlan(UpdateSubscriptionPlanDto inputModel);
    
    Task<IResult> DeleteSubscriptionPlan(Guid subscriptionPlanId);
    
    Task<IResult> DeleteSubscriptionPlanType(Guid subscriptionPlanTypeId);
}