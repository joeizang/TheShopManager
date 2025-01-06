using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class FairlyUsedItem
{
    public Guid Id { get; set; }

    public Guid PurchaseId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ProductId { get; set; }

    public DateTime DateBought { get; set; }

    public string UserId { get; set; } = null!;

    public Guid ShopId { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemDescription { get; set; } = null!;

    public string ItemDetails { get; set; } = null!;

    public decimal PriceAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Shop Shop { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
