using AutoMapper;
using DeliveryAPI.Domain;
using DeliveryAPI.Models.Requests;

namespace DeliveryAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateDeliveryRequest, Delivery>();
        }
    }
}
