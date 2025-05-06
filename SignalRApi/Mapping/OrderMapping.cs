using AutoMapper;
using SignalR.DtoLayer.OrderDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, GetOrderDto>().ReverseMap();
            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<Order, ResultOrderForKitchenDto>()
                .ForMember(x => x.MenuTableID, opt => opt.MapFrom(src => src.MenuTable!.MenuTableID))
                .ForMember(x => x.MenuTableName, opt => opt.MapFrom(src => src.MenuTable!.Name))
                .ForMember(x => x.MenuTableStatus, opt => opt.MapFrom(src => src.MenuTable!.Status))
                .ForMember(x => x.OrderDate, opt => opt.MapFrom(src => src.Date))
                .ReverseMap();
        }
    }
}
