using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class SubscriptionPlanType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Features { get; set; } = null!;

    public decimal Discount { get; set; }

    public decimal PriceAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<SubscriptionPlan> SubscriptionPlans { get; set; } = new List<SubscriptionPlan>();

    public virtual ICollection<TenantInvoice> TenantInvoices { get; set; } = new List<TenantInvoice>();
}
