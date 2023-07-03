using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentService
    {
        Task<DataResponseInfo<List<GetCommentDTO>>> GetAllComments();

        Task<DataResponseInfo<GetCommentDTO>> GetCommentById(int id);

        Task<ResponseInfo> AddComment(AddCommentDTO addProductDto);

        Task<ResponseInfo> UpdateComment(UpdateCommentDTO updateProductDto);

        Task<ResponseInfo> RemoveCommentById(int id);

        Task<DataResponseInfo<List<GetCommentDTO>>> GetAllCommentsByProductId(int id);
    }
}
