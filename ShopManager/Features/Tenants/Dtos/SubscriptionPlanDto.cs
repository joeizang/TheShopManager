using ShopManager.DomainModels;

namespace ShopManager.Features.Tenants.Dtos;

public record SubscriptionPlanDto(Guid TenantId, string TenantName, Guid SubscriptionPlanTypeId, 
    string SubscriptionPlanTypeName, decimal Price, Currency PaymentCurrency,
    BillingCycle BillingCycle, ActivationStatus Status);