using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentService
    {
        Task<DataResponseInfo<List<GetCommentDTO>>> GetAllCommentsAsync();

        Task<DataResponseInfo<GetCommentDTO>> GetCommentByIdAsync(int id);

        Task<ResponseInfo> AddCommentAsync(AddCommentDTO addProductDto);

        Task<ResponseInfo> UpdateCommentAsync(UpdateCommentDTO updateProductDto);

        Task<ResponseInfo> RemoveCommentByIdAsync(int id);

        Task<DataResponseInfo<List<GetCommentDTO>>> GetAllCommentsByProductIdAsync(int id);
    }
}
