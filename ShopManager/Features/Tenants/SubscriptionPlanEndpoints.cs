using ShopManager.Core;
using ShopManager.Features.Tenants.Filters;

namespace ShopManager.Features.Tenants;

public static class SubscriptionPlanEndpoints
{
    public static RouteGroupBuilder MapSubscriptionPlanEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var subscriptionPlanGroup = endpoints.MapGroup($"{Constants.V1}/subscriptionplans");
        var subscriptionPlanGroupWithId = subscriptionPlanGroup.MapGroup("/{subscriptionPlanId:guid}");
        
        // subscriptionPlanGroup.MapGet("/all", EndpointHandlers.GetSubscriptionPlans);
        // subscriptionPlanGroup.MapGet("", EndpointHandlers.GetCursoredSubscriptionPlans);
        // subscriptionPlanGroupWithId.MapGet("", EndpointHandlers.GetSubscriptionPlanById)
        //     .Produces<SubscriptionPlanDto>(200)
        //     .Produces(404);

        subscriptionPlanGroup.MapPost("", EndpointHandlers.CreateSubscriptionPlan)
            .AddEndpointFilter<FilterCreateSubscriptionPlan>()
            .Produces(201)
            .Produces(400)
            .Produces(500);

        subscriptionPlanGroupWithId.MapGet("", EndpointHandlers.GetSubscriptionPlanById)
            .Produces(200)
            .Produces(404);

        subscriptionPlanGroupWithId.MapDelete("", EndpointHandlers.DeleteSubscriptionPlan)
        .AddEndpointFilter<FilterDeleteSubscriptionPlan>()
        .Produces(204)
        .Produces(400)
        .Produces(404);

        return subscriptionPlanGroup;
    }
}