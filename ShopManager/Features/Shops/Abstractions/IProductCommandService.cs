using LanguageExt;
using ShopManager.Features.Shops.Products;

namespace ShopManager.Features.Shops.Abstractions;

public interface IProductCommandService
{
    Task<Option<ProductsDto>> AddProduct(AddProductDto dto, CancellationToken token);
    
    Task<Option<ProductsDto>> UpdateProduct(Guid productId, AddProductDto dto, CancellationToken token);
    
    Task<Option<ProductsDto>> DeleteProduct(Guid productId, CancellationToken token);
}