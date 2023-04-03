using AutoMapper;
using UserAPI.Domain;
using UserAPI.Models.Requests;

namespace UserAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateIdentityRequest, Identity>();
        }
    }
}
