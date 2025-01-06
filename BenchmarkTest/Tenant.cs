using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class Tenant
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string BillingAddress { get; set; } = null!;

    public DateTime NextBillingDate { get; set; }

    public DateTime SubscriptionStartDate { get; set; }

    public DateTime SubscriptionEndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();

    public virtual ICollection<SubscriptionPlan> SubscriptionPlans { get; set; } = new List<SubscriptionPlan>();

    public virtual ICollection<TenantInvoice> TenantInvoices { get; set; } = new List<TenantInvoice>();

    public virtual ICollection<TenantPaymentMethod> TenantPaymentMethods { get; set; } = new List<TenantPaymentMethod>();

    public virtual ICollection<TenantPayment> TenantPayments { get; set; } = new List<TenantPayment>();
}
