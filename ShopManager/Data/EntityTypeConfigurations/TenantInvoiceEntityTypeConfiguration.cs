using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManager.Features.Tenants.DomainModels;

namespace ShopManager.Data.EntityTypeConfigurations;

public class TenantInvoiceEntityTypeConfiguration : IEntityTypeConfiguration<TenantInvoice>
{
    public void Configure(EntityTypeBuilder<TenantInvoice> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.DueDate)
            .IsRequired();
        builder.ComplexProperty(x => x.AmountDue)
            .IsRequired();
        builder.Property(x => x.InvoiceReference)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Description)
            .HasMaxLength(300)
            .IsRequired();
        builder.Property(x => x.InvoiceStatus)
            .IsRequired();
        builder.HasOne(x => x.Tenant)
            .WithMany(x => x.TenantInvoices)
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(x => x.InvoiceReference)
            .IsUnique();
        builder.HasIndex(x => x.DueDate);
    }
}