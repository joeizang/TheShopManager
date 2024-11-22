using Microsoft.AspNetCore.Mvc;
using NodaTime.Text;
using ShopManager.Core;
using ShopManager.Data;

namespace ShopManager.Features.Tenants;

public static class TenantsEndpoint
{
    public static RouteGroupBuilder MapTenantEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var tenantGroup = endpoints.MapGroup($"{Constants.V1}/tenants");
        var tenantGroupWithId = tenantGroup.MapGroup("/{tenantId:guid}");
        
        tenantGroup.MapGet("", EndpointHandlers.GetTenants);

        tenantGroup.MapPost("", EndpointHandlers.CreateTenant);

        tenantGroupWithId.MapDelete("", async (ShopManagerBaseContext context, [FromBody] DeleteTenantDto dto) =>
        {
            var target = await context.Tenants.FindAsync(dto.TenantId).ConfigureAwait(false);
            if (target is null)
            {
                return Results.NotFound();
            }
            
            context.Tenants.Remove(target);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return Results.NoContent();
        }).Produces(204).Produces(400);

        return tenantGroup;
    }
}