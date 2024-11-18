using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class TenantPaymentEntityTypeConfiguration : IEntityTypeConfiguration<TenantPayment>
{
    public void Configure(EntityTypeBuilder<TenantPayment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.PaymentDate)
            .IsRequired();
        builder.ComplexProperty(x => x.AmountPaid)
            .IsRequired();
        builder.Property(x => x.PaymentReference)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Description)
            .HasMaxLength(300)
            .IsRequired();
        builder.Property(x => x.Status)
            .IsRequired();
        builder.HasOne(x => x.PaymentMethod)
            .WithMany(x => x.TenantPayments)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Tenant)
            .WithMany(x => x.TenantPayments)
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.PaymentReference);
        builder.HasIndex(x => x.PaymentDate);
        
    }
}