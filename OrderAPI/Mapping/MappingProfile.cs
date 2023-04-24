using AutoMapper;
using OrderAPI.Domain;
using OrderAPI.Models.Requests;
using SharedLibrary.Messages;

namespace OrderAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<OrderItem, Item>().ForMember(dest => dest.DeliveryId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
