using FluentValidation;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;

namespace ModsenOnlineStore.Store.Domain.Validators.CommentValidators
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentDTO>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
