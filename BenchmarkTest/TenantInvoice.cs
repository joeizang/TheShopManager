using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class TenantInvoice
{
    public Guid Id { get; set; }

    public DateTime DueDate { get; set; }

    public string InvoiceReference { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid SubscriptionPlanTypeId { get; set; }

    public Guid TenantId { get; set; }

    public decimal AmountDueAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual SubscriptionPlanType SubscriptionPlanType { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<TenantPayment> TenantPayments { get; set; } = new List<TenantPayment>();
}
