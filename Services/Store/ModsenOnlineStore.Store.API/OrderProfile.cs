using AutoMapper;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<AddOrderDTO, Order>();

            CreateMap<UpdateOrderDTO, Order>();

            CreateMap<UpdateOrderDTO, AddOrderDTO>();

            CreateMap<Order, GetOrderDTO>();
        }
    }
}
