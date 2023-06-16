using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentService
    {
        Task<ResponseInfo<List<GetCommentDto>>> GetAllComments();

        Task<ResponseInfo<GetCommentDto>> GetCommentById(int id);

        Task<NoDataResponseInfo> AddComment(AddCommentDto addProductDto);

        Task<NoDataResponseInfo> UpdateComment(UpdateCommentDto updateProductDto);

        Task<NoDataResponseInfo> RemoveCommentById(int id);
    }
}
