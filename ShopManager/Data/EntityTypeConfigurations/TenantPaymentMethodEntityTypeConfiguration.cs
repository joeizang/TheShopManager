using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.Features.Tenants.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class TenantPaymentMethodEntityTypeConfiguration : IEntityTypeConfiguration<TenantPaymentMethod>
{
    public void Configure(EntityTypeBuilder<TenantPaymentMethod> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.PaymentDetails)
            .HasMaxLength(300)
            .IsRequired();
        
        builder.Property(x => x.Version).IsRowVersion();
        
        builder.Property(x => x.IsDefaultPaymentMethod)
            .IsRequired();
        builder.HasOne(x => x.Tenant)
            .WithMany(x => x.PaymentMethods)
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}