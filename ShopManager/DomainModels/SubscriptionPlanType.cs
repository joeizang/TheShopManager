namespace ShopManager.DomainModels;

public class SubscriptionPlanType : BaseDomainModel
{
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public Money Price { get; set; } = new(Currency.NGN, 0m);
    
    public BillingCycle BillingCycle { get; set; }
    
    public string Features { get; set; } = string.Empty;

    public decimal Discount { get; set; }

    public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; } = [];
}