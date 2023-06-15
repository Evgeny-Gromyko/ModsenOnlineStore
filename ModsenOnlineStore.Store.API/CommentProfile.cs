using AutoMapper;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.API
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<AddCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
            CreateMap<Comment, GetCommentDto>();
        }
    }
}
