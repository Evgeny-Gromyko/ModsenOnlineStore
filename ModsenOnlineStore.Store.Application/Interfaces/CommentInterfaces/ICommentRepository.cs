using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment?> GetCommentById(int id);
        Task<List<Comment>> AddComment(Comment product);
        Task<List<Comment>> UpdateComment(Comment product);
        Task<List<Comment>> RemoveCommentById(int id);
    }
}
