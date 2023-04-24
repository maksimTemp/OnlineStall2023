using AutoMapper;
using CatalogAPI.Domain;
using CatalogAPI.Models.Requests;
using SharedLibrary.Messages;

namespace CatalogAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCreateRequest, Product>();
            CreateMap<ProducerCreateRequest, Producer>();
            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<Product, ItemChangedMessage>()
                .ForMember(src => src.EntityId, x => x.MapFrom(prod => prod.Id));
            
        }
    }
}
