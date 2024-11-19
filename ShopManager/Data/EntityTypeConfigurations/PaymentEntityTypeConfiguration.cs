using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.ComplexProperty(p => p.AmountPaid)
            .IsRequired();

        builder.Property(p => p.PaymentDate)
            .IsRequired();
        
        builder.Property(p => p.PaymentMethod)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.HasOne(p => p.Sale)
            .WithMany()
            .HasForeignKey(p => p.SalesId)
            .IsRequired();

        builder.HasIndex(p => p.ShopId);

        builder.HasIndex(p => p.SalesId);

        builder.HasIndex(p => p.PaymentDate);
    }
}
