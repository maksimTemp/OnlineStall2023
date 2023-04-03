using UserAPI.Context;
using UserAPI.Contracts.Requests;
using UserAPI.Contracts.Responses;
using UserAPI.Domain;
using UserAPI.Models.Requests;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserAPI.DataContext;

namespace AuthorizationApi.Services
{
    public interface IAuthService
    {
        Task<RegistrationResponse> RegisterAsync(RegisterRequest request);
        Task<AuthenticationResponse> LogInAsync(LoginRequest request);
        Task<AuthenticationResponse> RefreshAsync(RefreshTokenRequest request);
        Task<string> GetRole(string userToken);
        Task ConsumeUserChangedMessage(ConsumeContext<UserDataChangedMessage> consumeContext);
    }

    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UsersDataContext _dbContext;

        public AuthService(UsersDataContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterRequest request)
        {
            var pwHash = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(request.Password)));
            var userExists = await _userContext.Users.FirstOrDefaultAsync(x => (x.Email == request.Email) && (x.PWHash == pwHash));
            if (userExists != null)
                return new RegistrationResponse
                {
                    Success = false,
                    Errors = new[] { "User with this email already exist" }
                };
            var refreshToken = _tokenService.GenerateRefreshToken();

            var newUser = new User()
            {
                Email = request.Email,
                PWHash = pwHash,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(7),
                Role = request?.Role ?? UserRolesEnum.User
            };

            await _userContext.Users.AddAsync(newUser);
            await _userContext.SaveChangesAsync();

            return new RegistrationResponse
            {
                Success = true
            };
        }

        public async Task<AuthenticationResponse> LogInAsync(LoginRequest request)
        {
            var pwHash = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(request.Password)));
            var user = await _userContext.Users.FirstOrDefaultAsync(x => (x.Email == request.Email) );

            if (user is null)
                return new AuthenticationResponse
                {
                    Success = false,
                    Errors = new[] { "Invalid user data" }
                }; 
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim("Role", user.Role.ToString()),
                //new Claim("Test", "Test")
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _userContext.SaveChangesAsync();
            return new AuthenticationResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                Success = true
            };
        }

        public async Task<AuthenticationResponse> RefreshAsync(RefreshTokenRequest request)
        {
            string accessToken = request.AccessToken;
            string refreshToken = request.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var email = principal.Identity.Name;
            var user = await _userContext.Users.SingleOrDefaultAsync(x => x.Email == email);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return new AuthenticationResponse()
                {
                    Success = false,
                    Errors = new[] { "Invalid client request" }
                };
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userContext.SaveChangesAsync();

            return new AuthenticationResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                Success = true
            };
        }

        public async Task<string> GetRole(string userToken)
        {
            return _tokenService.GetRole(userToken);
        }

        public async Task ConsumeUserChangedMessage(ConsumeContext<UserDataChangedMessage> consumeContext)
        {
            var user = _userContext.Users.First(x => x.Id == consumeContext.Message.EntityId);
            user.Email = consumeContext.Message.Email;
            _userContext.Users.Update(user);
            await _userContext.SaveChangesAsync();
        }
    }
}
