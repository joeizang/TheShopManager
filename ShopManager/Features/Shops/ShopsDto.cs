using NodaTime;

namespace ShopManager.Features.Shops;

public record ShopDto(string ShopName, string ShopPhoneNumber, bool ShopStatus, string ShopAddress,
    Guid ShopId, Instant CreatedDate);

public record CreateShopDto(string ShopName, string ShopPhoneNumber, 
    string ShopAddress, string ShopLogo);