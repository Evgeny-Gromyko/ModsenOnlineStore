using FluentValidation;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;

namespace ModsenOnlineStore.Store.Domain.Validators.CommentValidators
{
    public class AddCommentValidator : AbstractValidator<AddCommentDTO>
    {
        public AddCommentValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
