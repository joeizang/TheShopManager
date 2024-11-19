using NodaTime;

namespace ShopManager.DomainModels;

public class Tenant : BaseDomainModel
{
    public string Name { get; set; } = string.Empty;

    public string ContactName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;
    
    public string PhoneNumber { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;

    public string BillingAddress { get; set; } = string.Empty;

    public bool ActivationStatus { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public ZonedDateTime NextBillingDate { get; set; }
    
    public ZonedDateTime SubscriptionStartDate { get; set; }
    
    public ZonedDateTime SubscriptionEndDate { get; set; }
    public ICollection<TenantPaymentMethod> PaymentMethods { get; set; } = [];
    
    public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; } = [];

    public ICollection<Shop> Shops { get; set; } = [];
    
    public ICollection<TenantInvoice> TenantInvoices { get; set; } = [];
    
    public ICollection<TenantPayment> TenantPayments { get; set; } = [];
}