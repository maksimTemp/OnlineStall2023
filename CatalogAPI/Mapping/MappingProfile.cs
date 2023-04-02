using AutoMapper;
using CatalogAPI.Domain;
using CatalogAPI.Models.Requests;

namespace CatalogAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCreateRequest, Product>();
            CreateMap<ProducerCreateRequest, Producer>();
            CreateMap<CategoryCreateRequest, Category>();
        }
    }
}
