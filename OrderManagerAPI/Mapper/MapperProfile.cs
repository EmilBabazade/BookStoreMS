using AutoMapper;
using OrderManagerAPI.Order.Dtos;

namespace OrderManagerAPI.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Order.Book, GetBookDto>();
        CreateMap<Order.Order, GetOrderDto>();
    }
}
