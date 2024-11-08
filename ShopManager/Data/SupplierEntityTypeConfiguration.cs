using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data;

public class SupplierEntityTypeConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder.Property(s => s.SupplierName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.SupplierEmailAddress)
            .HasMaxLength(100);

        builder.Property(s => s.SupplierPhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.SupplierAddress)
            .HasMaxLength(200);

        builder.HasMany(s => s.Products)
            .WithOne(p => p.Supplier)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(s => new { s.SupplierName, s.ShopId })
            .IsUnique();
    }
}
