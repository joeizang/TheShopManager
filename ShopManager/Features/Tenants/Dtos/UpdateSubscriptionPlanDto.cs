using ShopManager.DomainModels;

namespace ShopManager.Features.Tenants.Dtos;

public record UpdateSubscriptionPlanDto(Guid SubscriptionPlanTypeId, Guid TenantId, Guid SubscriptionPlanId);

public record UpdateSubscriptionPlanTypeDto(Guid SubscriptionPlanTypeId, string Name, decimal Price, 
    Currency PaymentCurrency, BillingCycle BillingCycle, ActivationStatus Status);