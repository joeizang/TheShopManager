using FluentValidation;

namespace ShopManager.Features.Tenants.Validations;

public class ValidateCreateTenant : AbstractValidator<CreateTenantDto>
{
    public ValidateCreateTenant()
    {
        
    }
}
