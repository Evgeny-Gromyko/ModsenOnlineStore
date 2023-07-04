using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync(int pageNumber, int pageSize);

        Task<Comment?> GetCommentByIdAsync(int id);

        Task AddCommentAsync(Comment product);

        Task UpdateCommentAsync(Comment product);

        Task RemoveCommentByIdAsync(int id);

        Task<List<Comment>> GetAllCommentsByProductIdAsync(int id, int pageNumber, int pageSize);
    }
}
