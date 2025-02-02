using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.Core;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class ProductEntityTypeConfiguration(IShopManagerContextAccessor shopManagerContextAccessor) : IEntityTypeConfiguration<Product>
{
    private readonly IShopManagerContextAccessor _shopManagerContextAccessor = shopManagerContextAccessor;

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        builder.HasQueryFilter(x => x.ShopId == _shopManagerContextAccessor.ShopId);
        builder.HasKey(p => p.Id);
        
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.ProductName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.ProductDescription)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(x => x.Version).IsRowVersion();

        builder.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(50);

        builder.ComplexProperty(p => p.CostPrice)
            .IsRequired();

        builder.ComplexProperty(p => p.SellingPrice)
            .IsRequired();

        builder.Property(p => p.IsFairlyUsed)
            .IsRequired();

        builder.HasOne(p => p.Supplier)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.SupplierId);

        builder.HasMany(p => p.Categories)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.FairlyUsedItem)
            .WithMany()
            .HasForeignKey(p => p.FairlyUsedItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Shop)
            .WithMany()
            .HasForeignKey(p => p.ShopId);
        
        builder.HasIndex(p => p.Sku)
            .IsUnique();
        builder.HasIndex(p => p.ShopId);
        builder.HasIndex(p => p.SupplierId);
        builder.HasIndex(p => p.CreatedAt);
    }
}
