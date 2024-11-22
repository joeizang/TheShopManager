using Microsoft.AspNetCore.Mvc;
using NodaTime.Text;
using ShopManager.Data;

namespace ShopManager.Features.Tenants;

public static class EndpointHandlers
{
    public static async Task<IResult> CreateTenant([FromServices] ITenantCommandService command, 
        [FromBody] CreateTenantDto inputModel)
    {
        var result = await command.CreateTenant(inputModel).ConfigureAwait(false);
        return Results.Created();
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
}