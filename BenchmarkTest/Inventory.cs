using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class Inventory
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public double QuantityInStock { get; set; }

    public double ReOrderLevel { get; set; }

    public double ReOrderQuantity { get; set; }

    public Guid ShopId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
