using System;
using ShopManager.Data;

namespace ShopManager.Features.Shops;

public class ShopsCommandService(ShopManagerBaseContext context) : IShopCommandService
{
    private readonly ShopManagerBaseContext _context = context;

    public async Task<ShopDto> CreateShop(CreateShopDto model)
    {
        var shop = model.MapToShop();
        _context.Shops.Add(shop);
        await _context.SaveChangesAsync();
        return shop.MapToShopDto();
    }
}
