using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        //private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository commentRepository, /*IProductRepository productRepository,*/ IMapper mapper)
        {
            this.commentRepository = commentRepository;
            //this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseInfo<List<GetCommentDto>>> GetAllComments()
        {
            var comments = await commentRepository.GetAllComments();
            var commentDtos = comments.Select(mapper.Map<GetCommentDto>).ToList();
            return new ResponseInfo<List<GetCommentDto>>(commentDtos, true, "all comments");
        }

        public async Task<ResponseInfo<GetCommentDto>> GetCommentById(int id)
        {
            var comment = await commentRepository.GetCommentById(id);

            if (comment is null)
            {
                return new ResponseInfo<GetCommentDto>(null, true, "comment");
            }

            var commentDto = mapper.Map<GetCommentDto>(comment);
            return new ResponseInfo<GetCommentDto>(commentDto, true, "comment");
        }

        public async Task<ResponseInfo<List<GetCommentDto>>> AddComment(AddCommentDto addCommentDto)
        {
            /*var product = await productRepository.GetProductById(addCommentDto.ProductId);

            if (product is null)
            {
                var response = await GetAllComments();
                response.Success = false;
                response.Message = "no such product";
                return response;
            }*/

            var comment = mapper.Map<Comment>(addCommentDto);
            await commentRepository.AddComment(comment);
            return await GetAllComments();
        }

        public async Task<ResponseInfo<List<GetCommentDto>>> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            /*var product = await productRepository.GetProductById(updateCommentDto.ProductId);

            if (product is null)
            {
                var response = await GetAllComments();
                response.Success = false;
                response.Message = "no such product";
                return response;
            }*/

            var comment = mapper.Map<Comment>(updateCommentDto);
            await commentRepository.AddComment(comment);
            return await GetAllComments();
        }

        public async Task<ResponseInfo<List<GetCommentDto>>> RemoveCommentById(int id)
        {
            await commentRepository.RemoveCommentById(id);
            return await GetAllComments();
        }
    }
}
