using FluentValidation;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;

namespace ModsenOnlineStore.Store.Domain.Validators.OrderValidators
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.DeliveryAddress).NotEmpty();
            RuleFor(x => x.TotalPrice).GreaterThanOrEqualTo(0);
        }
    }
}
