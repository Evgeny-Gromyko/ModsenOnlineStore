using FluentValidation;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;

namespace ModsenOnlineStore.Store.Domain.Validators.ProductValidators
{
    public class AddProductValidator : AbstractValidator<AddProductDTO>
    {
        public AddProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Discount).InclusiveBetween(0, 100);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
