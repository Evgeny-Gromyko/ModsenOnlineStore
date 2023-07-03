using FluentValidation;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;

namespace ModsenOnlineStore.Store.Domain.Validators.ProductValidators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Discount).ExclusiveBetween(0, 100);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
