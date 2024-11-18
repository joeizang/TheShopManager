namespace ShopManager.DomainModels;

public class SubscriptionPlan : BaseDomainModel
{
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public Money Price { get; set; } = new(Currency.NGN, 0m);

    public ActivationStatus Status { get; set; }

    public BillingCycle BillingCycle { get; set; }

    public string Features { get; set; } = string.Empty;
    
    public Guid TenantId { get; set; }
    
    public Tenant Tenant { get; set; } = default!;
}