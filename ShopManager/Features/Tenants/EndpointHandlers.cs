using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Text;
using ShopManager.Data;
using ShopManager.Features.Tenants.Abstractions;

namespace ShopManager.Features.Tenants;

public static class EndpointHandlers
{
    public static async Task<IResult> CreateTenant([FromServices] ITenantCommandService command, 
        [FromBody] CreateTenantDto inputModel)
    {
        try
        {
            var result = await command.CreateTenant(inputModel).ConfigureAwait(false);
            return TypedResults.Created();
        }
        catch (Exception e)
        {
            // Log the exception
            return TypedResults.InternalServerError();
        }
    }

    public static async Task<IResult> GetTenantByIdAsync(Guid tenantId, [FromServices] ShopManagerBaseContext context)
    {
        try
        {
            var tenant = await TenantsQueryService.GetTenantById(context, tenantId);
            return tenant is not null ? TypedResults.Ok(tenant) : TypedResults.NotFound();
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError();
        }
    }
    
    public static async Task<IResult> GetTenants([FromServices] ShopManagerBaseContext context)
    {
        var result = new List<TenantDto>();
        await foreach (var tenant in TenantsQueryService.GetTenants(context))
        {
            result.Add(tenant);
        }
        return TypedResults.Ok(result);
    }

    public static async Task<IResult> GetCursoredTenants([FromQuery] string cursor, [FromServices] ShopManagerBaseContext context)
    {
        if (InstantPattern.ExtendedIso.Parse(cursor.ToString()) is not { Success: true, Value: var parsedCursor })
        {
            return Results.BadRequest("Invalid cursor");
        }
        var result = new List<TenantDto>();
        await foreach(var tenant in TenantsQueryService.GetCursoredTenants(context, parsedCursor))
        {
            result.Add(tenant);
        }
        return TypedResults.Ok(result);
    }
    
    public static async Task<IResult> DeleteTenant(Guid tenantId, [FromServices] ITenantCommandService command)
    {
        try
        {
            var result = await command.DeleteTenant(tenantId).ConfigureAwait(false);
            return result;
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError();
        }
    }
}