using System.Text.Json;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

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
            shop.CreatedAt);
    
    public static Shop MapToShop(this UpdateShopDto dto) => new Shop
    {
        ShopName = dto.ShopName,
        ShopPhoneNumber = dto.ShopPhoneNumber,
        ShopAddress = dto.ShopAddress,
        ShopLogo = dto.ShopLogo ?? string.Empty,
        ShopEmailAddress = dto.ShopEmailAddress,
        ShopDescription = dto.ShopDescription,
        CacRegistrationNumber = dto.CacRegNumber,
        TaxIdentificationNUmber = dto.TaxId,
        Id = dto.ShopId
    };
}