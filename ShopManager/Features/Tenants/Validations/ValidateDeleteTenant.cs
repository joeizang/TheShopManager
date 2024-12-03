using FluentValidation;

namespace ShopManager.Features.Tenants.Validations;

public class ValidateDeleteTenant : AbstractValidator<DeleteTenantDto>
{
    public ValidateDeleteTenant()
    {
        RuleFor(x => x.TenantId).NotEmpty()
            .WithMessage("Tenant Id is required");
        RuleFor(x => x.TenantId).LessThanOrEqualTo(Guid.Empty)
            .WithMessage("Tenant Id must not be empty");
    }
}