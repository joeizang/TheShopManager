using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Tenants.DomainModels;

public class TenantPaymentMethod : BaseDomainModel
{
    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = default!;

    public List<TenantPayment> TenantPayments { get; set; } = [];

    public string PaymentDetails { get; set; } = string.Empty;

    public bool IsDefaultPaymentMethod { get; set; }

    public PaymentMethod PaymentMethod { get; set; } = default!;
}