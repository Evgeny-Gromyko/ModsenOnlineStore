using FluentValidation;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;

namespace ModsenOnlineStore.Store.Domain.Validators.OrderValidators
{
    public class AddOrderValidator : AbstractValidator<AddOrderDTO>
    {
        public AddOrderValidator()
        {
            RuleFor(x => x.DeliveryAddress).NotEmpty();
            RuleFor(x => x.TotalPrice).GreaterThan(0);
        }
    }
}
