using FluentValidation;
using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;

namespace ModsenOnlineStore.Login.Domain.Validators.UserValidators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
