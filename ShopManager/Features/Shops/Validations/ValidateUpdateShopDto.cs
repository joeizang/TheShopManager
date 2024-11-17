using FluentValidation;

namespace ShopManager.Features.Shops.Validations;

public class ValidateUpdateShopDto : AbstractValidator<UpdateShopDto>
{
    public ValidateUpdateShopDto()
    {
        RuleFor(x => x.ShopName).NotEmpty().WithMessage("Shop name is required");
        RuleFor(x => x.ShopPhoneNumber).NotEmpty().WithMessage("Phone number is required");
        RuleFor(x => x.ShopAddress).NotEmpty().WithMessage("Shop address is required");
        RuleFor(x => x.ShopEmailAddress).NotEmpty().WithMessage("Shop email address is required");
        RuleFor(x => x.ShopLogo).NotEmpty().WithMessage("Shop logo is required");
        RuleFor(x => x.ShopDescription).NotEmpty().WithMessage("Shop description is required");
        RuleFor(x => x.CacRegNumber).NotEmpty().WithMessage("CAC registration number is required");
        RuleFor(x => x.TaxId).NotEmpty().WithMessage("Tax identification number is required");
    }
}