using System;
using Microsoft.AspNetCore.Mvc;
using NodaTime.Text;
using ShopManager.Data;

namespace ShopManager.Features.Products;

public static class ProductsEndpoint
{
    public static RouteGroupBuilder MapProductsEndpoints(this IEndpointRouteBuilder routes)
    {
        var productsGroup = routes.MapGroup("/api/v1/products");
        var productsGroupWithIds = productsGroup.MapGroup("/{shopId:Guid}");

        productsGroup.MapGet("", async ([FromServices]ShopManagerBaseContext context,
            [FromQuery]string cursor) =>
        {
            if (!Guid.TryParse(cursor, out Guid parsedCursor))
            {
                return Results.BadRequest("Invalid cursor");
            }
            var products = new List<object>();

            await foreach (var product in ProductsQueryService.GetProducts(context, parsedCursor))
            {
                products.Add(product);
            }

            return Results.Ok(products);
        });

        return productsGroup;
    }
}
