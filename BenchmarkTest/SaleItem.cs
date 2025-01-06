using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class SaleItem
{
    public Guid Id { get; set; }

    public Guid SaleId { get; set; }

    public Guid ShopId { get; set; }

    public Guid ProductId { get; set; }

    public double QuantitySold { get; set; }

    public decimal TotalAmountAmount { get; set; }

    public decimal UnitPriceAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
