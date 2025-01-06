using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class Customer
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public Guid ShopId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<FairlyUsedItem> FairlyUsedItems { get; set; } = new List<FairlyUsedItem>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual Shop Shop { get; set; } = null!;
}
