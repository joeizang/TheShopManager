using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopManager.Core;
using ShopManager.Data;
using ShopManager.Features.Shops.Abstractions;

namespace ShopManager.Features.Shops.Products;

public static class EndpointHandler
{
    public static async Task<IResult> GetProducts(Guid shopId,
        string cursor, [FromServices] ShopManagerBaseContext context, CancellationToken token)
    {
        var result = await ProductsQueryService
            .GetProducts(context, shopId, cursor.ToInstantDate(), token)
            .ConfigureAwait(false);
        return TypedResults.Ok(result);
    }
    
    public static async Task<IResult> GetAllProducts([FromServices] ShopManagerBaseContext context, Guid shopId,
        CancellationToken token)
    {
        var result = await ProductsQueryService
            .GetAllProducts(context, shopId, token)
            .ConfigureAwait(false);
        return TypedResults.Ok(result);
    }
    
    public static async Task<IResult> GetProductsByName(Guid shopId, string name,
        [FromServices] ShopManagerBaseContext context, CancellationToken token)
    {
        var result = await ProductsQueryService
            .GetProductsByName(context, name, shopId, token)
            .ConfigureAwait(false);
        return TypedResults.Ok(result);
    }
    
    public static async Task<IResult> GetProductByCategory(Guid shopId, Guid categoryId,
        [FromServices] ShopManagerBaseContext context, CancellationToken token)
    {
        var result = await ProductsQueryService
            .GetProductByCategory(context, shopId, categoryId, token)
            .ConfigureAwait(false);
        return TypedResults.Ok(result);
    }
    
    public static async Task<IResult> GetProductById(Guid shopId, Guid productId,
        [FromServices] ShopManagerBaseContext context, CancellationToken token)
    {
        var result = await ProductsQueryService
            .GetProductById(context, shopId, productId, token)
            .ConfigureAwait(false);
        return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }
    
    public static async Task<IResult> CreateProduct(Guid shopId, [FromBody] AddProductDto dto,
        [FromServices] IProductCommandService service, CancellationToken token)
    {
        var result = await service.AddProduct(dto, token).ConfigureAwait(false);
        return result.Match<IResult>((x) =>
            TypedResults.Created("", x),
            () => TypedResults.BadRequest("Failed to create product"));
    }
    
    public static async Task<IResult> UpdateProduct(Guid shopId, Guid productId, [FromBody] AddProductDto dto,
        [FromServices] IProductCommandService service, CancellationToken token)
    {
        var result = await service.UpdateProduct(shopId, productId, dto, token).ConfigureAwait(false);
        return result.Match<IResult>(TypedResults.Ok, TypedResults.NotFound);
    }

    public static async Task<IResult> DeleteProduct(Guid shopId, Guid productId,
        [FromServices] IProductCommandService service, CancellationToken token)
    {
        var result = await service.DeleteProduct(shopId, productId, token).ConfigureAwait(false);
        return result.Match<IResult>(TypedResults.Ok, TypedResults.NotFound);
    }
}
