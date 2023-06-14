using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentService
    {
        Task<ResponseInfo<List<GetCommentDto>>> GetAllComments();
        Task<ResponseInfo<GetCommentDto>> GetCommentById(int id);
        Task<ResponseInfo<List<GetCommentDto>>> AddComment(AddCommentDto addProductDto);
        Task<ResponseInfo<List<GetCommentDto>>> UpdateComment(UpdateCommentDto updateProductDto);
        Task<ResponseInfo<List<GetCommentDto>>> RemoveCommentById(int id);
    }
}
