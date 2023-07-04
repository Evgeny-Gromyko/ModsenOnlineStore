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

        public async Task<DataResponseInfo<List<GetCommentDTO>>> GetAllCommentsAsync(int pageNumber, int pageSize)
        {
            var comments = await commentRepository.GetAllCommentsAsync(pageNumber, pageSize);
            var commentDtos = comments.Select(mapper.Map<GetCommentDTO>).ToList();

            return new DataResponseInfo<List<GetCommentDTO>>(data: commentDtos, success: true, message: "all comments");
        }

        public async Task<DataResponseInfo<GetCommentDTO>> GetCommentByIdAsync(int id)
        {
            var comment = await commentRepository.GetCommentByIdAsync(id);

            if (comment is null)
            {
                return new DataResponseInfo<GetCommentDTO>(data: null, success: false, message: "no such comment");
            }

            var commentDto = mapper.Map<GetCommentDTO>(comment);

            return new DataResponseInfo<GetCommentDTO>(data: commentDto, success: true, message: "comment");
        }

        public async Task<ResponseInfo> AddCommentAsync(AddCommentDTO addCommentDto)
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

        public async Task<ResponseInfo> UpdateCommentAsync(UpdateCommentDTO updateCommentDto)
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

        public async Task<DataResponseInfo<List<GetCommentDTO>>> GetAllCommentsByProductIdAsync(int id, int pageNumber, int pageSize)
        {
            var product = await productRepository.GetProductByIdAsync(id);

            if (product is null)
            {
                return new DataResponseInfo<List<GetCommentDTO>>(data: null, success: false, message: "no such product");
            }

            var productComments = await commentRepository.GetAllCommentsByProductIdAsync(id, pageNumber, pageSize);
            var commentDtos = productComments.Select(mapper.Map<GetCommentDTO>).ToList();

            return new DataResponseInfo<List<GetCommentDTO>>(data: commentDtos, success: true, message: "all comments of product");
        }
    }
}
