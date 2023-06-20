namespace ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs
{
    public class AddCommentDto
    {
        public string Text { get; set; } = string.Empty;

        public int ProductId { get; set; }

        public int UserId { get; set; }
    }
}
