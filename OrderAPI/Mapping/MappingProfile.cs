using AutoMapper;
using OrderAPI.Domain;
using OrderAPI.Models.Requests;

namespace OrderAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrderRequest, Order>();
        }
    }
}
