using NodaTime;

namespace ShopManager.Features.Shops;

public record ShopDto(string ShopName, string ShopPhoneNumber, bool ShopStatus, string ShopAddress,
    Guid ShopId, Instant CreatedDate, Guid TenantId);

public record CreateShopDto(string ShopName, string ShopPhoneNumber, 
    string ShopAddress, string ShopLogo, string ShopDescription, string ShopEmail, string CacRegistrationNumber,
    string TaxIdentificationNumber, Guid TenantId);
    
public record UpdateShopDto(Guid ShopId, Guid TenantId, string ShopName, string ShopAddress, string ShopPhoneNumber, 
    string ShopEmailAddress, string ShopLogo, string ShopDescription, string CacRegNumber, string TaxId);
    
public record DeleteShopDto(Guid ShopId, Guid TenantId);    