using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();

        Task<Comment?> GetCommentByIdAsync(int id);

        Task AddCommentAsync(Comment product);

        Task UpdateCommentAsync(Comment product);

        Task RemoveCommentByIdAsync(int id);
    }
}
