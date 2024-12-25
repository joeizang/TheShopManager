using NodaTime;
using ShopManager.CustomExceptions;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Tenants.DomainModels;

public class TenantPayment : BaseDomainModel
{
    public Instant PaymentDate { get; set; }

    public Money AmountPaid { get; set; } = new(Currency.NGN, 0m);

    public string PaymentReference { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public PaymentStatus Status { get; set; }

    public Guid TenantInvoiceId { get; set; }
    
    public TenantInvoice TenantInvoice { get; set; } = default!;

    public Guid PaymentMethodId { get; set; }

    public TenantPaymentMethod PaymentMethod { get; set; } = default!;

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = default!;

    public TenantPayment(Guid paymentMethodId)
    {
        if (paymentMethodId == Guid.Empty)
        {
            throw new NoPaymentMethodException("You must provide a payment method before making a payment!");
        }
        PaymentMethodId = paymentMethodId;
    }
}