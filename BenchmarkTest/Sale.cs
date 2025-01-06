using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class Sale
{
    public Guid Id { get; set; }

    public Guid SaleId { get; set; }

    public DateTime SaleDate { get; set; }

    public Guid? CustomerId { get; set; }

    public string SalesPersonId { get; set; } = null!;

    public Guid ShopId { get; set; }

    public decimal TotalAmountAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Payment> PaymentSales { get; set; } = new List<Payment>();

    public virtual ICollection<Payment> PaymentSalesNavigation { get; set; } = new List<Payment>();

    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    public virtual AspNetUser SalesPerson { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
