using AutoMapper;
using UserAPI.DataContext;
using UserAPI.Domain;
using UserAPI.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace UserAPI.Services
{
    public class IdentitiesService : IIdentityService
    {
        private readonly UsersDataContext _dbContext;
        private readonly IMapper _mapper;

        public IdentitiesService(UsersDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        
    }
    public interface IIdentityService : IService<Identity>
    {
        Task<Identity> CreateAsync(CreateIdentityRequest order);
    }
}
