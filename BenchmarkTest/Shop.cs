using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class Shop
{
    public Guid Id { get; set; }

    public string ShopName { get; set; } = null!;

    public string ShopAddress { get; set; } = null!;

    public string ShopPhoneNumber { get; set; } = null!;

    public string ShopEmailAddress { get; set; } = null!;

    public string ShopLogo { get; set; } = null!;

    public string ShopDescription { get; set; } = null!;

    public string CacRegistrationNumber { get; set; } = null!;

    public string TaxIdentificationNumber { get; set; } = null!;

    public bool Status { get; set; }

    public Guid TenantId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<FairlyUsedItem> FairlyUsedItems { get; set; } = new List<FairlyUsedItem>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

    public virtual Tenant Tenant { get; set; } = null!;
}
