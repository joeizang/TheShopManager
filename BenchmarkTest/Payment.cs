using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid SalesId { get; set; }

    public Guid ShopId { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentReference { get; set; } = null!;

    public Guid? SaleId { get; set; }

    public decimal AmountPaidAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Sale? Sale { get; set; }

    public virtual Sale Sales { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
