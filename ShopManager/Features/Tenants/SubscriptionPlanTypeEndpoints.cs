using ShopManager.Core;
using ShopManager.Features.Tenants.Filters;

namespace ShopManager.Features.Tenants;

public static class SubscriptionPlanTypeEndpoints
{
    public static RouteGroupBuilder MapSubscriptionPlanTypeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var subscriptionPlanTypeGroup = endpoints.MapGroup($"{Constants.V1}/subscriptionplantypes");
        var subscriptionPlanTypeGroupWithId = subscriptionPlanTypeGroup.MapGroup("/{subscriptionPlanTypeId:guid}");
        
        subscriptionPlanTypeGroup.MapGet("/all", EndpointHandlers.GetSubscriptionPlanTypes);
        subscriptionPlanTypeGroup.MapGet("", EndpointHandlers.GetCursoredSubscriptionPlanTypes);
        subscriptionPlanTypeGroupWithId.MapGet("", EndpointHandlers.GetSubscriptionPlanTypeById)
            .Produces<SubscriptionPlanTypeDto>(200)
            .Produces(404);

        subscriptionPlanTypeGroup.MapPost("", EndpointHandlers.CreateSubscriptionPlanType)
            .AddEndpointFilter<FilterCreateSubscriptionPlanType>()
            .Produces(201)
            .Produces(400)
            .Produces(500);

        subscriptionPlanTypeGroupWithId.MapGet("", EndpointHandlers.GetSubscriptionPlanTypeById)
            .Produces(200)
            .Produces(404);

        subscriptionPlanTypeGroupWithId.MapDelete("", EndpointHandlers.DeleteSubscriptionPlanType)
            .AddEndpointFilter<FilterDeleteSubscriptionPlanType>()
            .Produces(204)
            .Produces(400)
            .Produces(404);

        return subscriptionPlanTypeGroup;
    }
}