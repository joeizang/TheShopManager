using NodaTime;

namespace ShopManager.Features.Shops;

public record ShopDto(string ShopName, string ShopPhoneNumber, bool ShopStatus, string ShopAddress,
    Guid ShopId, Instant CreatedDate);

public record CreateShopDto(string ShopName, string ShopPhoneNumber, 
    string ShopAddress, string ShopLogo, string ShopDescription, string ShopEmail, string CacRegistrationNumber,
    string TaxIdentificationNumber);
    
    public record UpdateShopDto(Guid ShopId, string ShopName, string ShopAddress, string ShopPhoneNumber, 
        string ShopEmailAddress, string ShopLogo, string ShopDescription, 
        string CacRegNumber, string TaxId);