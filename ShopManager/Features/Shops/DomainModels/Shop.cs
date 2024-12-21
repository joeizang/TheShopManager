using ShopManager.DomainModels;
using ShopManager.Features.Tenants.DomainModels;

namespace ShopManager.Features.Shops.DomainModels;

public class Shop : BaseDomainModel
{
    public string ShopName { get; set; } = string.Empty;

    public string ShopAddress { get; set; } = string.Empty;

    public string ShopPhoneNumber { get; set; } = string.Empty;

    public string ShopEmailAddress { get; set; } = string.Empty;

    public string ShopLogo { get; set; } = string.Empty;

    public string ShopDescription { get; set; } = string.Empty;

    public string CacRegistrationNumber { get; set; } = string.Empty;

    public string TaxIdentificationNUmber { get; set; } = string.Empty;

    public bool Status { get; set; } = false;
    
    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = default!;
}