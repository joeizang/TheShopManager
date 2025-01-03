using System.Text.Json;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Shops;

public static class ShopExtensions
{
    public static Shop MapToShop(this CreateShopDto dto)
    {
        var shop = new Shop(dto.TenantId)
        {
            ShopName = dto.ShopName,
            ShopPhoneNumber = dto.ShopPhoneNumber,
            ShopAddress = dto.ShopAddress,
            ShopLogo = dto.ShopLogo,
            ShopEmailAddress = dto.ShopEmail,
            ShopDescription = dto.ShopDescription,
            CacRegistrationNumber = dto.CacRegistrationNumber,
            TaxIdentificationNUmber = dto.TaxIdentificationNumber
        };
        return shop;   
    }
    
    public static ShopDto MapToShopDto(this Shop shop) => new ShopDto(
            shop.ShopName,
            shop.ShopPhoneNumber,
            shop.Status,
            shop.ShopAddress,
            shop.Id,
            shop.CreatedAt, shop.TenantId);

    public static Shop MapToShop(this UpdateShopDto dto)
    {
        var shop = new Shop(dto.TenantId)
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
        return shop;
    }
}