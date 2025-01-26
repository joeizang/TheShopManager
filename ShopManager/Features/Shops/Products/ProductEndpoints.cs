using System;
using ShopManager.Features.Shops.Products.Filters;

namespace ShopManager.Features.Shops.Products;

public static class ProductEndpoints
{
    public static void AddProductEndpoints(RouteGroupBuilder shopGroupWithIds)
    {
        shopGroupWithIds.MapGet("/products", Products.EndpointHandler.GetProducts)
            .AddEndpointFilter<FilterGetProducts>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        
        shopGroupWithIds.MapGet("/products/all", Products.EndpointHandler.GetAllProducts)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        
        shopGroupWithIds.MapGet("/products/{productId:guid}", Products.EndpointHandler.GetProductById)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        
        shopGroupWithIds.MapGet("/products/categories/{categoryId:guid}", 
                Products.EndpointHandler.GetProductByCategory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        
        shopGroupWithIds.MapGet("/products/{name}", Products.EndpointHandler.GetProductsByName)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        
        shopGroupWithIds.MapPost("/products", Products.EndpointHandler.CreateProduct)
            .AddEndpointFilter<FilterCreateProduct>()
            .Produces<ProductsDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
        
        shopGroupWithIds.MapPut("/products/{productId:guid}", Products.EndpointHandler.UpdateProduct)
            .AddEndpointFilter<FilterUpdateProduct>()
            .Produces<ProductsDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
        
        shopGroupWithIds.MapDelete("/products/{productId:guid}", Products.EndpointHandler.DeleteProduct)
            .AddEndpointFilter<FilterDeleteProduct>()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
