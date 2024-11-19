using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.BillingAddress)
            .HasMaxLength(200);
        
        builder.Property(t => t.Address)
            .HasMaxLength(200);

        builder.Property(t => t.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(t => t.EmailAddress)
            .HasMaxLength(50);
        
        builder.Property(t => t.ContactName)
            .HasMaxLength(100);
        
        builder.Property(t => t.ActivationStatus)
            .HasDefaultValue(false)
            .IsRequired();
        
        builder.Property(t => t.PaymentStatus)
            .HasDefaultValue(PaymentStatus.UNINITIALIZED)
            .IsRequired();
        
        builder.Property(t => t.SubscriptionStartDate)
            .IsRequired();
        
        builder.HasMany(t => t.TenantPayments)
            .WithOne(p => p.Tenant)
            .HasForeignKey(p => p.TenantId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(t => t.SubscriptionPlans)
            .WithOne(sp => sp.Tenant)
            .HasForeignKey(sp => sp.TenantId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(t => t.Shops)
            .WithOne(s => s.Tenant)
            .HasForeignKey(s => s.TenantId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(t => t.TenantInvoices)
            .WithOne(i => i.Tenant)
            .HasForeignKey(i => i.TenantId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(t => t.PaymentMethods)
            .WithOne(pm => pm.Tenant)
            .HasForeignKey(pm => pm.TenantId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(t => t.EmailAddress)
            .IsUnique();
    }
}