namespace ShopManager.Features.Tenants;

public record CreateSubscriptionPlanDto(Guid TenantId, Guid SubscriptionPlanTypeId);