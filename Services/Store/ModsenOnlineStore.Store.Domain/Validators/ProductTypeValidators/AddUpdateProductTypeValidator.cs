using FluentValidation;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;

namespace ModsenOnlineStore.Store.Domain.Validators.ProductTypeValidators
{
    public class AddUpdateProductTypeValidator : AbstractValidator<AddUpdateProductTypeDTO>
    {
        public AddUpdateProductTypeValidator()
        {
            RuleFor(x => x.TypeName).NotEmpty();
        }
    }
}
