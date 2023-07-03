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

        public async Task<DataResponseInfo<List<GetCommentDto>>> GetAllCommentsAsync()
        {
            var comments = await commentRepository.GetAllCommentsAsync();
            var commentDtos = comments.Select(mapper.Map<GetCommentDto>).ToList();

            return new DataResponseInfo<List<GetCommentDto>>(data: commentDtos, success: true, message: "all comments");
        }

        public async Task<DataResponseInfo<GetCommentDto>> GetCommentByIdAsync(int id)
        {
            var comment = await commentRepository.GetCommentByIdAsync(id);

            if (comment is null)
            {
                return new DataResponseInfo<GetCommentDto>(data: null, success: false, message: "no such comment");
            }

            var commentDto = mapper.Map<GetCommentDto>(comment);

            return new DataResponseInfo<GetCommentDto>(data: commentDto, success: true, message: "comment");
        }

        public async Task<ResponseInfo> AddCommentAsync(AddCommentDto addCommentDto)
        {
            var product = await productRepository.GetProductByIdAsync(addCommentDto.ProductId);

            if (product is null)
            {
                return new ResponseInfo(success: false, message: "no such product");
            }

            var comment = mapper.Map<Comment>(addCommentDto);
            await commentRepository.AddCommentAsync(comment);

            return new ResponseInfo(success: true, message: "comment added");
        }

        public async Task<ResponseInfo> UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            var oldComment = await commentRepository.GetCommentByIdAsync(updateCommentDto.Id);

            if (oldComment is null)
            {
                return new ResponseInfo(success: false, message: "no such comment");
            }

            var product = await productRepository.GetProductByIdAsync(updateCommentDto.ProductId);

            if (product is null)
            {
                return new ResponseInfo(success: false, message: "no such product");
            }

            var comment = mapper.Map<Comment>(updateCommentDto);
            await commentRepository.UpdateCommentAsync(comment);

            return new ResponseInfo(success: true, message: "updated");
        }

        public async Task<ResponseInfo> RemoveCommentByIdAsync(int id)
        {
            var oldComment = await commentRepository.GetCommentByIdAsync(id);

            if (oldComment is null)
            {
                return new ResponseInfo(success: false, message: "no such comment");
            }

            await commentRepository.RemoveCommentByIdAsync(id);

            return new ResponseInfo(success: true, message: "removed");
        }

        public async Task<DataResponseInfo<List<GetCommentDto>>> GetAllCommentsByProductIdAsync(int id)
        {
            var product = await productRepository.GetProductByIdAsync(id);

            if (product is null)
            {
                return new DataResponseInfo<List<GetCommentDto>>(data: null, success: false, message: "no such product");
            }

            var comments = await commentRepository.GetAllCommentsAsync();
            var productComments = comments.FindAll(c => c.ProductId == id);
            var commentDtos = productComments.Select(mapper.Map<GetCommentDto>).ToList();

            return new DataResponseInfo<List<GetCommentDto>>(data: commentDtos, success: true, message: "all comments of product");
        }
    }
}
