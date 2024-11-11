namespace ShopManager.Features.Shops;

public record ShopDto(string shopName, string shopPhoneNumber, bool shopStatus, string shopAddress, Guid shopId);

public record CreateShopDto(string shopName, string shopPhoneNumber, string shopAddress);