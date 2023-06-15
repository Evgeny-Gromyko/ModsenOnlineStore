using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseInfo<List<GetCommentDto>>> GetAllComments()
        {
            var comments = await commentRepository.GetAllComments();
            var commentDtos = comments.Select(mapper.Map<GetCommentDto>).ToList();

            return new ResponseInfo<List<GetCommentDto>>(data: commentDtos, success: true, message: "all comments");
        }

        public async Task<ResponseInfo<GetCommentDto>> GetCommentById(int id)
        {
            var comment = await commentRepository.GetCommentById(id);

            if (comment is null)
            {
                return new ResponseInfo<GetCommentDto>(data: null, success: true, message: "comment");
            }

            var commentDto = mapper.Map<GetCommentDto>(comment);

            return new ResponseInfo<GetCommentDto>(data: commentDto, success: true, message: "comment");
        }

        public async Task<NoDataResponseInfo> AddComment(AddCommentDto addCommentDto)
        {
            var comment = mapper.Map<Comment>(addCommentDto);
            await commentRepository.AddComment(comment);

            return new NoDataResponseInfo(success: true, message: "added successfully");
        }

        public async Task<NoDataResponseInfo> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            var oldComment = await commentRepository.GetCommentById(updateCommentDto.Id);

            if (oldComment is null)
            {
                return new NoDataResponseInfo(success: false, message: "no such comment");
            }

            var comment = mapper.Map<Comment>(updateCommentDto);
            await commentRepository.UpdateComment(comment);

            return new NoDataResponseInfo(success: true, message: "updated successfully");
        }

        public async Task<NoDataResponseInfo> RemoveCommentById(int id)
        {
            var oldComment = await commentRepository.GetCommentById(id);

            if (oldComment is null)
            {
                return new NoDataResponseInfo(success: false, message: "no such comment");
            }

            await commentRepository.RemoveCommentById(id);

            return new NoDataResponseInfo(success: true, message: "removed successfully");
        }
    }
}
