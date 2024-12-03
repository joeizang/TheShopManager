using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Text;
using ShopManager.Data;

namespace ShopManager.Features.Tenants;

public static class EndpointHandlers
{
    public static async Task<IResult> CreateTenant([FromServices] ITenantCommandService command, 
        [FromBody] CreateTenantDto inputModel)
    {
        try
        {
            var result = await command.CreateTenant(inputModel).ConfigureAwait(false);
            return Results.Created();
        }
        catch (Exception e)
        {
            // Log the exception
            return Results.InternalServerError();
        }
    }
    
    public static async Task<IResult> GetTenants([FromServices] ShopManagerBaseContext context)
    {
        var result = new List<TenantDto>();
        await foreach (var tenant in TenantsQueryService.GetTenants(context))
        {
            result.Add(tenant);
        }
        return Results.Ok(result);
    }

    public static async Task<IResult> GetCursoredTenants([FromServices] ShopManagerBaseContext context,
    Instant cursor)
    {
        var result = new List<TenantDto>();
        await foreach(var tenant in TenantsQueryService.GetCursoredTenants(context, cursor))
        {
            result.Add(tenant);
        }
        return Results.Ok(result);
    }
}