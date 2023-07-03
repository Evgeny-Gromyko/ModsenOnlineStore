using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;

namespace ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces
{
    public interface ICommentService
    {
        Task<DataResponseInfo<List<GetCommentDto>>> GetAllCommentsAsync();

        Task<DataResponseInfo<GetCommentDto>> GetCommentByIdAsync(int id);

        Task<ResponseInfo> AddCommentAsync(AddCommentDto addProductDto);

        Task<ResponseInfo> UpdateCommentAsync(UpdateCommentDto updateProductDto);

        Task<ResponseInfo> RemoveCommentByIdAsync(int id);

        Task<DataResponseInfo<List<GetCommentDto>>> GetAllCommentsByProductIdAsync(int id);
    }
}
