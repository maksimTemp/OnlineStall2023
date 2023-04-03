using UserAPI.Domain;

namespace UserAPI.Models.Requests
{
    public class CreateIdentityRequest
    {
        public string Email { get; set; }
        public string PWHash { get; set; }
        public UserRolesEnum Role { get; set; }
    }
}
