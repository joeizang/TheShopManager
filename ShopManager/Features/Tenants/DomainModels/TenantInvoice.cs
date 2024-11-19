using NodaTime;
using ShopManager.DomainModels;

namespace ShopManager.Features.Tenants.DomainModels;

public class TenantInvoice : BaseDomainModel
{
    public ZonedDateTime DueDate { get; set; }

    public Money AmountDue { get; set; } = new(Currency.NGN, 0m);

    public string InvoiceReference { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public InvoiceStatus Status { get; set; }

    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = default!;
}