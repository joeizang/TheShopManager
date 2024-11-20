using System;
using Microsoft.EntityFrameworkCore;
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

    public async Task<ShopDto> UpdateShop(UpdateShopDto model)
    {
        var shop = model.MapToShop();
        _context.Entry(shop).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return shop.MapToShopDto();
    }
    
    public async Task DeleteShop(Guid shopId)
    {
        var shop = await _context.Shops.FindAsync(shopId);
        shop.IsDeleted = true;
        _context.Entry(shop).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
