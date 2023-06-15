using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentService
    {
        Task<ResponseInfo<List<GetCommentDto>>> GetAllComments();

        Task<ResponseInfo<GetCommentDto>> GetCommentById(int id);

        Task<ResponseInfo<string>> AddComment(AddCommentDto addProductDto);

        Task<ResponseInfo<string>> UpdateComment(UpdateCommentDto updateProductDto);

        Task<ResponseInfo<string>> RemoveCommentById(int id);
    }
}
