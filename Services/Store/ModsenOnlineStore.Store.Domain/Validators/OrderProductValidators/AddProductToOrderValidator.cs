using FluentValidation;
using ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;

namespace ModsenOnlineStore.Store.Domain.Validators.OrderProductValidators
{
    public class AddProductToOrderValidator : AbstractValidator<AddProductToOrderDTO>
    {
        public AddProductToOrderValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
