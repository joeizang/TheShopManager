using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class SaleItemEntityTypeConfiguration : IEntityTypeConfiguration<SaleItems>
{
    public void Configure(EntityTypeBuilder<SaleItems> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.QuantitySold)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(s => s.UnitPrice)
            .IsRequired();

        builder.ComplexProperty(s => s.UnitPrice);

        builder.Property(s => s.TotalAmount)
            .IsRequired();

        builder.ComplexProperty(s => s.TotalAmount);

        builder.HasOne(s => s.Product)
            .WithMany()
            .HasForeignKey(s => s.ProductId);

        builder.HasOne(s => s.Sale)
            .WithMany(s => s.SaleItems)
            .HasForeignKey(s => s.SaleId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(p => p.ShopId);
    }
}
