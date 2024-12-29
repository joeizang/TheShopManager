using ShopManager.Core;
using ShopManager.Features.Tenants.Dtos;
using ShopManager.Features.Tenants.Filters;

namespace ShopManager.Features.Tenants;

public static class TenantPaymentMethodEndpoints
{
    public static RouteGroupBuilder MapTenantPaymentMethodEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var tenantPaymentMethodGroup = endpoints.MapGroup($"{Constants.V1}/tenants/paymentmethods");
        var tenantPaymentMethodGroupWithId = tenantPaymentMethodGroup.MapGroup("/{tenantId:guid}");

        tenantPaymentMethodGroup.MapPost("", EndpointHandlers.CreateTenantPaymentMethod)
            .AddEndpointFilter<FilterCreateTenantPaymentMethod>()
            .Produces(201)
            .Produces(400)
            .Produces(500);

        tenantPaymentMethodGroupWithId.MapGet("", EndpointHandlers.GetTenantPaymentMethods)
            .Produces<TenantPaymentMethodDto>(200)
            .Produces(404);

        tenantPaymentMethodGroupWithId.MapDelete("", EndpointHandlers.DeleteTenantPaymentMethod)
            .AddEndpointFilter<FilterDeleteTenantPaymentMethod>()
            .Produces(204)
            .Produces(400)
            .Produces(404);

        return tenantPaymentMethodGroup;
    }
    
}