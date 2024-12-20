using System;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using ShopManager.Data;

namespace ShopManager.Features.Shops;

public class ShopsCommandService(ShopManagerBaseContext context) : IShopCommandService
{
    private readonly ShopManagerBaseContext _context = context;

    public async Task<Result<ShopDto>> CreateShop(CreateShopDto model)
    {
        var shop = model.MapToShop();
        _context.Shops.Add(shop);
        await _context.SaveChangesAsync();
        return new Result<ShopDto>(shop.MapToShopDto());
    }

    public async Task<Result<ShopDto>> UpdateShop(UpdateShopDto model)
    {
        var shop = model.MapToShop();
        _context.Entry(shop).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return new Result<ShopDto>(shop.MapToShopDto());
    }
    
    public async Task<Result<IResult>> DeleteShop(Guid shopId)
    {
        var shop = await _context.Shops.FindAsync(shopId);
        if (shop is null)
        {
            return new Result<IResult>(TypedResults.NotFound());
        }
        shop.IsDeleted = true;
        _context.Entry(shop).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return new Result<IResult>(TypedResults.NoContent());
    }
}
