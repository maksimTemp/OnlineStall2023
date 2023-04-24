using AutoMapper;
using SharedLibrary.Messages;
using DeliveryAPI.Domain;
using DeliveryAPI.Models.Requests;

namespace DeliveryAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateDeliveryRequest, Delivery>();
            CreateMap<DeliveryCreateMessage, Delivery>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EntityId))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<Item, DeliveryItem>()
                .ForMember(dest => dest.DeliveryId, opt => opt.MapFrom(src => (Guid)src.DeliveryId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
