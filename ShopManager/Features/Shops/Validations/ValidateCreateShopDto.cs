using FluentValidation;

namespace ShopManager.Features.Shops.Validations;

public class ValidateCreateShopDto : AbstractValidator<CreateShopDto>
{
    public ValidateCreateShopDto()
    {
        RuleFor(x => x.ShopAddress)
            .MaximumLength(300)
            .NotEmpty();
        RuleFor(x => x.ShopPhoneNumber)
            .MaximumLength(20)
            .NotEmpty();
        RuleFor(x => x.ShopName)
            .MaximumLength(100)
            .NotEmpty();
        RuleFor(x => x.ShopDescription)
            .MaximumLength(300);
        RuleFor(x => x.CacRegistrationNumber)
            .MaximumLength(20)
            .NotEmpty();
        RuleFor(x => x.TaxIdentificationNumber)
            .MaximumLength(20)
            .NotEmpty();
        RuleFor(x => x.ShopEmail)
            .EmailAddress()
            .MaximumLength(100)
            .NotEmpty();
        RuleFor(x => x.ShopLogo)
            .MaximumLength(500);
    }
}