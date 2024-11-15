using ShopManager.DomainModels;

namespace ShopManager.Features.Shops;

public static class ShopExtensions
{
    public static Shop MapToShop(this CreateShopDto dto) => new Shop
    {
        ShopName = dto.ShopName,
        ShopPhoneNumber = dto.ShopPhoneNumber,
        ShopAddress = dto.ShopAddress,
        ShopLogo = dto.ShopLogo ?? string.Empty,
    };
    
    public static ShopDto MapToShopDto(this Shop shop) => new ShopDto(
        shop.ShopName,
        shop.ShopPhoneNumber,
        shop.Status,
        shop.ShopAddress,
        shop.Id,
        shop.CreatedAt
    );
}