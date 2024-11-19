namespace ShopManager.Features.Shops;

public interface IShopCommandService
{
    Task<ShopDto> CreateShop(CreateShopDto model);
    
    Task<ShopDto> UpdateShop(UpdateShopDto model);
}
