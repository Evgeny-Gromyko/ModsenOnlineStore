using AutoMapper;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.API
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<AddCommentDTO, Comment>();
            CreateMap<UpdateCommentDTO, Comment>();
            CreateMap<Comment, GetCommentDTO>();
        }
    }
}
