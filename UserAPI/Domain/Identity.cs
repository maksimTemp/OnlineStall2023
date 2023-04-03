using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Domain
{
    public class Identity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PWHash { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public UserRolesEnum Role { get; set; }
    }
}
