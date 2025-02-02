namespace ShopManager.Features.Shops.Sales;

public static class SalesEndpoint
{
    public static RouteGroupBuilder AddSalesEndpoints(RouteGroupBuilder shopGroupWithIds)
    {
        shopGroupWithIds.MapGet("/sales", Sales.EndpointHandler.GetSales)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        _ = shopGroupWithIds.MapGet("/sales/{saleId:guid}", Sales.EndpointHandler.GetSaleById)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        
        // shopGroupWithIds.MapPost("/sales", Sales.EndpointHandler.CreateSale)
        //     .Produces(StatusCodes.Status201Created)
        //     .Produces(StatusCodes.Status400BadRequest);
        
        // shopGroupWithIds.MapPut("/sales/{saleId:guid}", Sales.EndpointHandler.UpdateSale)
        //     .Produces(StatusCodes.Status204NoContent)
        //     .Produces(StatusCodes.Status400BadRequest);
        
        // shopGroupWithIds.MapDelete("/sales/{saleId:guid}", Sales.EndpointHandler.DeleteSale)
        //     .Produces(StatusCodes.Status204NoContent)
        //     .Produces(StatusCodes.Status404NotFound);

        return shopGroupWithIds;
    }
}
