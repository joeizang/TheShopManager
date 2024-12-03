using Microsoft.AspNetCore.Mvc;
using NodaTime.Text;
using ShopManager.Core;
using ShopManager.Data;
using ShopManager.Features.Tenants.Filters;

namespace ShopManager.Features.Tenants;

public static class TenantsEndpoint
{
    public static RouteGroupBuilder MapTenantEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var tenantGroup = endpoints.MapGroup($"{Constants.V1}/tenants");
        var tenantGroupWithId = tenantGroup.MapGroup("/{tenantId:guid}");
        
        tenantGroup.MapGet("/all", EndpointHandlers.GetTenants);
        tenantGroup.MapGet("", EndpointHandlers.GetCursoredTenants);

        tenantGroup.MapPost("", EndpointHandlers.CreateTenant)
            .AddEndpointFilter<FilterCreateTenant>()
            .Produces(201)
            .Produces(400)
            .Produces(500);

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
        })
        .AddEndpointFilter<FilterDeleteTenant>()
        .Produces(204)
        .Produces(400);

        return tenantGroup;
    }
}