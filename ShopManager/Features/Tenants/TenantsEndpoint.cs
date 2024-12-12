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
        tenantGroupWithId.MapGet("/subscriptionplans", EndpointHandlers.GetSubscriptionPlansForTenant)
            .Produces<SubscriptionPlanDto>(200)
            .Produces(404);

        tenantGroup.MapPost("", EndpointHandlers.CreateTenant)
            .AddEndpointFilter<FilterCreateTenant>()
            .Produces(201)
            .Produces(400)
            .Produces(500);

        tenantGroupWithId.MapGet("", EndpointHandlers.GetTenantByIdAsync)
            .Produces<TenantDto>(200)
            .Produces(404);

        tenantGroupWithId.MapDelete("", EndpointHandlers.DeleteTenant)
        .AddEndpointFilter<FilterDeleteTenant>()
        .Produces(204)
        .Produces(400)
        .Produces(404);

        return tenantGroup;
    }
}