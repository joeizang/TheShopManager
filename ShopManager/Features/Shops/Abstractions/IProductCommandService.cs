using LanguageExt;
using ShopManager.Features.Shops.Products;

namespace ShopManager.Features.Shops.Abstractions;

public interface IProductCommandService
{
    Task<Option<ProductsDto>> AddProduct(AddProductDto dto, CancellationToken token);
    
    Task<Option<ProductsDto>> UpdateProduct(Guid shopId, Guid productId, AddProductDto dto, CancellationToken token);
    
    Task<Option<IResult>> DeleteProduct(Guid shopId, Guid productId, CancellationToken token);
}