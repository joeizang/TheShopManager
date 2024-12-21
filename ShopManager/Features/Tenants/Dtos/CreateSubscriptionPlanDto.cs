namespace ShopManager.Features.Tenants.Dtos;

public record CreateSubscriptionPlanDto(Guid TenantId, Guid SubscriptionPlanTypeId);