using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentService
    {
        Task<DataResponseInfo<List<GetCommentDto>>> GetAllComments();

        Task<DataResponseInfo<GetCommentDto>> GetCommentById(int id);

        Task<ResponseInfo> AddComment(AddCommentDto addProductDto);

        Task<ResponseInfo> UpdateComment(UpdateCommentDto updateProductDto);

        Task<ResponseInfo> RemoveCommentById(int id);
    }
}
