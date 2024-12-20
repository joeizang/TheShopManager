using LanguageExt.Common;

namespace ShopManager.Features.Shops;

public interface IShopCommandService
{
    Task<Result<ShopDto>> CreateShop(CreateShopDto model);
    
    Task<Result<ShopDto>> UpdateShop(UpdateShopDto model);

    Task<Result<IResult>> DeleteShop(Guid shopId);
}
