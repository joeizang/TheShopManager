using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class SaleItemEntityTypeConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder.Property(s => s.QuantitySold)
            .HasPrecision(10, 2)
            .IsRequired();
        
        builder.Property(x => x.Version).IsRowVersion();

        builder.ComplexProperty(s => s.UnitPrice)
            .IsRequired();

        builder.ComplexProperty(s => s.TotalAmount)
            .IsRequired();

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
