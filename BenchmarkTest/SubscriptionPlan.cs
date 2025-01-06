using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class SubscriptionPlan
{
    public Guid Id { get; set; }

    public Guid SubscriptionPlanTypeId { get; set; }

    public Guid TenantId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual SubscriptionPlanType SubscriptionPlanType { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
