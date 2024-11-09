using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class InventoryEntityTypeConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();

        builder.Property(i => i.QuantityInStock)
            .IsRequired();

        builder.Property(i => i.ReOrderLevel)
            .IsRequired();

        builder.Property(i => i.ReOrderQuantity)
            .IsRequired();

        builder.HasOne(i => i.Product)
            .WithOne()
            .HasForeignKey<Inventory>(i => i.ProductId)
            .IsRequired();
            

        builder.HasOne(i => i.Shop)
            .WithMany()
            .HasForeignKey(i => i.ShopId);

        builder.HasIndex(i => i.ProductId);

        builder.HasIndex(i => i.ShopId);

        builder.HasIndex(i => i.CreatedAt);
    }
}
