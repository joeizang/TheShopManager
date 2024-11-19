using ShopManager.DomainModels;

namespace ShopManager.Features.Tenants.DomainModels;

public class SubscriptionPlan : BaseDomainModel
{
    public ActivationStatus Status { get; set; }

    public Guid SubscriptionPlanTypeId { get; set; }

    public SubscriptionPlanType SubscriptionPlanType { get; set; } = default!;

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = default!;
}