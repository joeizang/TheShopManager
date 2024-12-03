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

    public static async Task<IResult> GetTenantByIdAsync(Guid tenantId, [FromServices] ShopManagerBaseContext context)
    {
        try
        {
            var tenant = await TenantsQueryService.GetTenantById(context, tenantId);
            return tenant is not null ? Results.Ok(tenant) : Results.NotFound();
        }
        catch (Exception e)
        {
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

    public static async Task<IResult> GetCursoredTenants(Instant cursor, [FromServices] ShopManagerBaseContext context)
    {
        var result = new List<TenantDto>();
        await foreach(var tenant in TenantsQueryService.GetCursoredTenants(context, cursor))
        {
            result.Add(tenant);
        }
        return Results.Ok(result);
    }
}