using LanguageExt;
using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Features.Shops.Abstractions;
using ShopManager.Features.Shops.Products;

namespace ShopManager.Features.Shops.Services;

public class ProductCommandService(ShopManagerBaseContext context) : IProductCommandService
{
    public async Task<Option<ProductsDto>> AddProduct(AddProductDto dto, CancellationToken token)
    {
        var product = dto.MapToProduct();
        context.Products.Add(product);
        await context.SaveChangesAsync(token);
        return Option<ProductsDto>.Some(product.MapToProductsDto());
    }

    public async Task<Option<ProductsDto>> UpdateProduct(Guid productId, AddProductDto dto, CancellationToken token)
    {
        var product = await context.Products.FindAsync(new object?[] { productId }, cancellationToken: token)
            .ConfigureAwait(false);
        if (product is null)
        {
            return Option<ProductsDto>.None;
        }
        product.UpdateProduct(dto);
        context.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync(token).ConfigureAwait(false);
        return Option<ProductsDto>.Some(product.MapToProductsDto());
    }

    public async Task<Option<ProductsDto>> DeleteProduct(Guid productId, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}