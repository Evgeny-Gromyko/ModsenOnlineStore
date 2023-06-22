using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces;
using ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository commentRepository, IProductRepository productRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<DataResponseInfo<List<GetCommentDto>>> GetAllComments()
        {
            var comments = await commentRepository.GetAllComments();
            var commentDtos = comments.Select(mapper.Map<GetCommentDto>).ToList();

            return new DataResponseInfo<List<GetCommentDto>>(data: commentDtos, success: true, message: "all comments");
        }

        public async Task<DataResponseInfo<GetCommentDto>> GetCommentById(int id)
        {
            var comment = await commentRepository.GetCommentById(id);

            if (comment is null)
            {
                return new DataResponseInfo<GetCommentDto>(data: null, success: false, message: "no such comment");
            }

            var commentDto = mapper.Map<GetCommentDto>(comment);

            return new DataResponseInfo<GetCommentDto>(data: commentDto, success: true, message: "comment");
        }

        public async Task<ResponseInfo> AddComment(AddCommentDto addCommentDto)
        {
            var product = await productRepository.GetProductById(addCommentDto.ProductId);

            if (product is null)
            {
                return new ResponseInfo(success: false, message: "no such product");
            }

            var comment = mapper.Map<Comment>(addCommentDto);
            await commentRepository.AddComment(comment);

            return new ResponseInfo(success: true, message: "comment added");
        }

        public async Task<ResponseInfo> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            var oldComment = await commentRepository.GetCommentById(updateCommentDto.Id);

            if (oldComment is null)
            {
                return new ResponseInfo(success: false, message: "no such comment");
            }

            var product = await productRepository.GetProductById(updateCommentDto.ProductId);

            if (product is null)
            {
                return new ResponseInfo(success: false, message: "no such product");
            }

            var comment = mapper.Map<Comment>(updateCommentDto);
            await commentRepository.UpdateComment(comment);

            return new ResponseInfo(success: true, message: "updated");
        }

        public async Task<ResponseInfo> RemoveCommentById(int id)
        {
            var oldComment = await commentRepository.GetCommentById(id);

            if (oldComment is null)
            {
                return new ResponseInfo(success: false, message: "no such comment");
            }

            await commentRepository.RemoveCommentById(id);

            return new ResponseInfo(success: true, message: "removed");
        }

        public async Task<DataResponseInfo<List<GetCommentDto>>> GetAllCommentsByProductId(int id)
        {
            var comments = await commentRepository.GetAllComments();
            var productComments = comments.FindAll(c => c.ProductId == id);
            var commentDtos = productComments.Select(mapper.Map<GetCommentDto>).ToList();

            return new DataResponseInfo<List<GetCommentDto>>(data: commentDtos, success: true, message: "all comments of product");
        }
    }
}
