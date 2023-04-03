using UserAPI.Domain;

namespace UserAPI.Models.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public UserRolesEnum Role { get; set; }
    }
}
