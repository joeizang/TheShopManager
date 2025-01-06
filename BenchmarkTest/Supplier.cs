using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class Supplier
{
    public Guid Id { get; set; }

    public string SupplierName { get; set; } = null!;

    public string SupplierAddress { get; set; } = null!;

    public string SupplierPhoneNumber { get; set; } = null!;

    public string SupplierEmailAddress { get; set; } = null!;

    public Guid ShopId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Shop Shop { get; set; } = null!;
}
