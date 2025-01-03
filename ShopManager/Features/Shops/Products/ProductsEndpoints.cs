using Microsoft.AspNetCore.Mvc;
using ShopManager.Data;

namespace ShopManager.Features.Shops.Products;

public static class ProductsEndpoint
{
    public static RouteGroupBuilder MapProductsEndpoints(this IEndpointRouteBuilder routes)
    {
        var productsGroup = routes.MapGroup("/api/v1/products");
        var productsGroupWithIds = productsGroup.MapGroup("/{shopId:Guid}");

        productsGroup.MapGet("", ([FromServices]ShopManagerBaseContext context,
            [FromQuery]string cursor) =>
        {
            Guid.TryParse(cursor, out Guid parsedCursor);
            var products= ProductsQueryService.GetProducts(context, parsedCursor);

            return Results.Ok(products);
        }).AddEndpointFilter((async (context, next) =>
        {
            var cursor = context.GetArgument<string>(1);
            Guid.TryParse(cursor, out Guid parsedCursor);
            return parsedCursor == Guid.Empty ? Results.BadRequest("Invalid Id") : await next(context);
        }));

        return productsGroup;
    }
}
