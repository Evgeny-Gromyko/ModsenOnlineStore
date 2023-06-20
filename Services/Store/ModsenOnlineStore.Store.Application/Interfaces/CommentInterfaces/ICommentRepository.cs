using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComments();

        Task<Comment?> GetCommentById(int id);

        Task AddComment(Comment product);

        Task UpdateComment(Comment product);

        Task RemoveCommentById(int id);
    }
}
