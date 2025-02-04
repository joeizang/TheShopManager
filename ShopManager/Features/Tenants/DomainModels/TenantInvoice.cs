using NodaTime;
using ShopManager.DomainModels;

namespace ShopManager.Features.Tenants.DomainModels;

public class TenantInvoice : BaseDomainModel
{
    public Instant DueDate { get; set; }

    public Money AmountDue { get; set; } = new(Currency.NGN, 0m);

    public string InvoiceReference { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public InvoiceStatus InvoiceStatus { get; set; } = InvoiceStatus.NOT_PAYED;

    public SubscriptionPlanType SubscriptionPlanType { get; set; } = null!;
    
    public Guid SubscriptionPlanTypeId { get; set; }

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = default!;
    
    public ICollection<TenantPayment> TenantPayments { get; set; } = [];

    public TenantInvoice()
    {
        AmountDue = Tenant.SubscriptionPlans.Single().SubscriptionPlanType.Price;
    }
    
}