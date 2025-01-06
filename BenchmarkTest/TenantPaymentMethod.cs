using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class TenantPaymentMethod
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string PaymentDetails { get; set; } = null!;

    public bool IsDefaultPaymentMethod { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<TenantPayment> TenantPayments { get; set; } = new List<TenantPayment>();
}
