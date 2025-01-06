using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkTest;

public partial class ShopplatformContext : DbContext
{
    public ShopplatformContext()
    {
    }

    public ShopplatformContext(DbContextOptions<ShopplatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<FairlyUsedItem> FairlyUsedItems { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleItem> SaleItems { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<SubscriptionPlanType> SubscriptionPlanTypes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<TenantInvoice> TenantInvoices { get; set; }

    public virtual DbSet<TenantPayment> TenantPayments { get; set; }

    public virtual DbSet<TenantPaymentMethod> TenantPaymentMethods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=shopplatform;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("activation_status", new[] { "active", "deactivated", "inactive", "suspended" })
            .HasPostgresEnum("billing_cycle", new[] { "monthly", "quarterly", "yearly" })
            .HasPostgresEnum("currency", new[] { "aud", "cad", "cny", "eur", "gbp", "ghs", "gmd", "gnf", "inr", "jpy", "kes", "lrd", "mwk", "mzn", "ngn", "rwf", "sll", "ugx", "usd", "xaf", "xof", "xpf", "zar", "zmw" })
            .HasPostgresEnum("fairlyused_item_condition", new[] { "excellent", "fair", "good", "poor", "very_good" })
            .HasPostgresEnum("invoice_status", new[] { "not_payed", "overdue", "partially_payed", "payed" })
            .HasPostgresEnum("payment_method", new[] { "bank_transfer", "cash", "cheque", "crypto_currency", "mobile_money", "not_set", "pos", "ussd" })
            .HasPostgresEnum("payment_status", new[] { "failed", "pending", "successful", "uninitialized" });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Discriminator).HasMaxLength(21);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.ShopId, "IX_AspNetUsers_ShopId");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Shop).WithMany(p => p.AspNetUsers).HasForeignKey(d => d.ShopId);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.CategoryName, "IX_Categories_CategoryName");

            entity.HasIndex(e => e.ProductId, "IX_Categories_ProductId");

            entity.HasIndex(e => e.ShopId, "IX_Categories_ShopId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Product).WithMany(p => p.Categories).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Shop).WithMany(p => p.Categories)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(e => e.EmailAddress, "IX_Customers_EmailAddress").IsUnique();

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "IX_Customers_FirstName_LastName");

            entity.HasIndex(e => e.PhoneNumber, "IX_Customers_PhoneNumber");

            entity.HasIndex(e => e.ShopId, "IX_Customers_ShopId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);

            entity.HasOne(d => d.Shop).WithMany(p => p.Customers).HasForeignKey(d => d.ShopId);
        });

        modelBuilder.Entity<FairlyUsedItem>(entity =>
        {
            entity.HasIndex(e => e.CreatedAt, "IX_FairlyUsedItems_CreatedAt");

            entity.HasIndex(e => e.CustomerId, "IX_FairlyUsedItems_CustomerId");

            entity.HasIndex(e => e.ProductId, "IX_FairlyUsedItems_ProductId");

            entity.HasIndex(e => e.ShopId, "IX_FairlyUsedItems_ShopId");

            entity.HasIndex(e => e.UserId, "IX_FairlyUsedItems_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ItemDescription).HasMaxLength(500);
            entity.Property(e => e.ItemDetails).HasMaxLength(1000);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.PriceAmount).HasColumnName("Price_amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.FairlyUsedItems).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Product).WithMany(p => p.FairlyUsedItems).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Shop).WithMany(p => p.FairlyUsedItems).HasForeignKey(d => d.ShopId);

            entity.HasOne(d => d.User).WithMany(p => p.FairlyUsedItems).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasIndex(e => e.CreatedAt, "IX_Inventories_CreatedAt");

            entity.HasIndex(e => e.ProductId, "IX_Inventories_ProductId").IsUnique();

            entity.HasIndex(e => e.ShopId, "IX_Inventories_ShopId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Product).WithOne(p => p.Inventory).HasForeignKey<Inventory>(d => d.ProductId);

            entity.HasOne(d => d.Shop).WithMany(p => p.Inventories).HasForeignKey(d => d.ShopId);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasIndex(e => e.PaymentDate, "IX_Payments_PaymentDate");

            entity.HasIndex(e => e.SaleId, "IX_Payments_SaleId");

            entity.HasIndex(e => e.SalesId, "IX_Payments_SalesId");

            entity.HasIndex(e => e.ShopId, "IX_Payments_ShopId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AmountPaidAmount).HasColumnName("AmountPaid_amount");

            entity.HasOne(d => d.Sale).WithMany(p => p.PaymentSales).HasForeignKey(d => d.SaleId);

            entity.HasOne(d => d.Sales).WithMany(p => p.PaymentSalesNavigation).HasForeignKey(d => d.SalesId);

            entity.HasOne(d => d.Shop).WithMany(p => p.Payments).HasForeignKey(d => d.ShopId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CreatedAt, "IX_Products_CreatedAt");

            entity.HasIndex(e => e.FairlyUsedItemId, "IX_Products_FairlyUsedItemId");

            entity.HasIndex(e => e.ShopId, "IX_Products_ShopId");

            entity.HasIndex(e => e.Sku, "IX_Products_Sku").IsUnique();

            entity.HasIndex(e => e.SupplierId, "IX_Products_SupplierId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CostPriceAmount).HasColumnName("CostPrice_amount");
            entity.Property(e => e.ProductDescription).HasMaxLength(500);
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.SellingPriceAmount).HasColumnName("SellingPrice_amount");
            entity.Property(e => e.Sku).HasMaxLength(50);

            entity.HasOne(d => d.FairlyUsedItem).WithMany(p => p.Products)
                .HasForeignKey(d => d.FairlyUsedItemId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Shop).WithMany(p => p.Products).HasForeignKey(d => d.ShopId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasIndex(e => e.CreatedAt, "IX_Sales_CreatedAt");

            entity.HasIndex(e => e.CustomerId, "IX_Sales_CustomerId");

            entity.HasIndex(e => e.SaleDate, "IX_Sales_SaleDate");

            entity.HasIndex(e => e.SalesPersonId, "IX_Sales_SalesPersonId");

            entity.HasIndex(e => e.ShopId, "IX_Sales_ShopId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.TotalAmountAmount).HasColumnName("TotalAmount_amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.SalesPerson).WithMany(p => p.Sales)
                .HasForeignKey(d => d.SalesPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Shop).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_SaleItems_ProductId");

            entity.HasIndex(e => e.SaleId, "IX_SaleItems_SaleId");

            entity.HasIndex(e => e.ShopId, "IX_SaleItems_ShopId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.TotalAmountAmount).HasColumnName("TotalAmount_amount");
            entity.Property(e => e.UnitPriceAmount).HasColumnName("UnitPrice_amount");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleItems).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Shop).WithMany(p => p.SaleItems).HasForeignKey(d => d.ShopId);
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasIndex(e => e.TenantId, "IX_Shops_TenantId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CacRegistrationNumber).HasMaxLength(20);
            entity.Property(e => e.ShopAddress).HasMaxLength(1000);
            entity.Property(e => e.ShopDescription).HasMaxLength(500);
            entity.Property(e => e.ShopEmailAddress).HasMaxLength(100);
            entity.Property(e => e.ShopLogo).HasMaxLength(500);
            entity.Property(e => e.ShopName).HasMaxLength(100);
            entity.Property(e => e.ShopPhoneNumber).HasMaxLength(22);
            entity.Property(e => e.TaxIdentificationNumber)
                .HasMaxLength(20)
                .HasColumnName("TaxIdentificationNUmber");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Shops)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasIndex(e => e.SubscriptionPlanTypeId, "IX_SubscriptionPlans_SubscriptionPlanTypeId");

            entity.HasIndex(e => e.TenantId, "IX_SubscriptionPlans_TenantId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.SubscriptionPlanType).WithMany(p => p.SubscriptionPlans)
                .HasForeignKey(d => d.SubscriptionPlanTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubscriptionPlans_SubscriptionPlanTypes_SubscriptionPlanTyp~");

            entity.HasOne(d => d.Tenant).WithMany(p => p.SubscriptionPlans)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SubscriptionPlanType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Features).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PriceAmount).HasColumnName("Price_amount");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasIndex(e => e.ShopId, "IX_Suppliers_ShopId");

            entity.HasIndex(e => new { e.SupplierName, e.ShopId }, "IX_Suppliers_SupplierName_ShopId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SupplierAddress).HasMaxLength(200);
            entity.Property(e => e.SupplierEmailAddress).HasMaxLength(100);
            entity.Property(e => e.SupplierName).HasMaxLength(100);
            entity.Property(e => e.SupplierPhoneNumber).HasMaxLength(20);

            entity.HasOne(d => d.Shop).WithMany(p => p.Suppliers).HasForeignKey(d => d.ShopId);
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasIndex(e => e.EmailAddress, "IX_Tenants_EmailAddress").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BillingAddress).HasMaxLength(200);
            entity.Property(e => e.ContactName).HasMaxLength(100);
            entity.Property(e => e.EmailAddress).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<TenantInvoice>(entity =>
        {
            entity.HasIndex(e => e.DueDate, "IX_TenantInvoices_DueDate");

            entity.HasIndex(e => e.InvoiceReference, "IX_TenantInvoices_InvoiceReference").IsUnique();

            entity.HasIndex(e => e.SubscriptionPlanTypeId, "IX_TenantInvoices_SubscriptionPlanTypeId");

            entity.HasIndex(e => e.TenantId, "IX_TenantInvoices_TenantId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AmountDueAmount).HasColumnName("AmountDue_amount");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.InvoiceReference).HasMaxLength(100);

            entity.HasOne(d => d.SubscriptionPlanType).WithMany(p => p.TenantInvoices).HasForeignKey(d => d.SubscriptionPlanTypeId);

            entity.HasOne(d => d.Tenant).WithMany(p => p.TenantInvoices)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TenantPayment>(entity =>
        {
            entity.HasIndex(e => e.PaymentDate, "IX_TenantPayments_PaymentDate");

            entity.HasIndex(e => e.PaymentMethodId, "IX_TenantPayments_PaymentMethodId");

            entity.HasIndex(e => e.PaymentReference, "IX_TenantPayments_PaymentReference");

            entity.HasIndex(e => e.TenantId, "IX_TenantPayments_TenantId");

            entity.HasIndex(e => e.TenantInvoiceId, "IX_TenantPayments_TenantInvoiceId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AmountPaidAmount).HasColumnName("AmountPaid_amount");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.PaymentReference).HasMaxLength(100);

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.TenantPayments)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Tenant).WithMany(p => p.TenantPayments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.TenantInvoice).WithMany(p => p.TenantPayments)
                .HasForeignKey(d => d.TenantInvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TenantPaymentMethod>(entity =>
        {
            entity.HasIndex(e => e.TenantId, "IX_TenantPaymentMethods_TenantId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PaymentDetails).HasMaxLength(300);

            entity.HasOne(d => d.Tenant).WithMany(p => p.TenantPaymentMethods)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
