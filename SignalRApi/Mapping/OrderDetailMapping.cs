using AutoMapper;
using SignalR.DtoLayer.OrderDetailDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping
{
    public class OrderDetailMapping : Profile
    {
        public OrderDetailMapping()
        {
            CreateMap<OrderDetail, CreateOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, GetOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, ResultOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, UpdateOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, ResultOrderDetailByOrderIdWithProducts>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product!.ImageUrl))
                .ReverseMap();
            CreateMap<OrderDetail, ResultOrderDetailForKitchenDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName))
                .ReverseMap();
            CreateMap<OrderDetail, ResultOrderDetailForPayment>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName))
                .ReverseMap();
        }
    }
}
