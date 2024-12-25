using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;
using ShopManager.Features.Tenants.Dtos;

namespace ShopManager.Features.Tenants.DomainModels;

public class TenantPaymentMethod : BaseDomainModel
{
    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = default!;

    // should this be a list of Guids?
    public List<TenantPayment> TenantPayments { get; set; } = [];

    public string PaymentDetails { get; set; } = string.Empty;

    public bool IsDefaultPaymentMethod { get; set; }
    

    public PaymentMethod PaymentMethod { get; set; } = default!;

    public void UpdatePaymentMethod(UpdateTenantPaymentMethodDto dto)
    {
        PaymentDetails = dto.PaymentDetails;
        PaymentMethod = dto.PaymentMethod;
        IsDefaultPaymentMethod = dto.IsDefaultPaymentMethod;
    }
}