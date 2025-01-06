using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class Product
{
    public Guid Id { get; set; }

    public Guid SupplierId { get; set; }

    public string Sku { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public bool IsFairlyUsed { get; set; }

    public Guid? FairlyUsedItemId { get; set; }

    public Guid ShopId { get; set; }

    public decimal CostPriceAmount { get; set; }

    public decimal SellingPriceAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual FairlyUsedItem? FairlyUsedItem { get; set; }

    public virtual ICollection<FairlyUsedItem> FairlyUsedItems { get; set; } = new List<FairlyUsedItem>();

    public virtual Inventory? Inventory { get; set; }

    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    public virtual Shop Shop { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
