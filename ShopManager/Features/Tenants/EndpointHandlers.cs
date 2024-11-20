using Microsoft.AspNetCore.Mvc;

namespace ShopManager.Features.Tenants;

public static class EndpointHandlers
{
    public static async Task<TenantDto> CreateTenant([FromServices] ITenantCommandService command,
        [FromBody] CreateTenantDto inputModel)
    {
        var result = await command.CreateTenant(inputModel).ConfigureAwait(false);
        return result;
    }
}