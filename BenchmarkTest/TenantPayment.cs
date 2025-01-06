using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class TenantPayment
{
    public Guid Id { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentReference { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid TenantInvoiceId { get; set; }

    public Guid PaymentMethodId { get; set; }

    public Guid TenantId { get; set; }

    public decimal AmountPaidAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual TenantPaymentMethod PaymentMethod { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual TenantInvoice TenantInvoice { get; set; } = null!;
}
