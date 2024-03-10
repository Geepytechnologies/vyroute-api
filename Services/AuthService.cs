using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using vyroute.Dto;
using vyroute.Models;

namespace vyroute.Services
{
    public class AuthService : IAuthService

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _config = config;
            _logger = logger;
        }

        public string GenerateRefreshTokenString()
        {
           var randomNumber = new byte[64];
            using(var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }
        public async Task<string> GenerateTokenstringAsync(LoginModel user)
        {
            var isAdmin = await GetUserAdminStatusAsync(user.Username);

            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name ,user.Username),
            };
            if (!isAdmin)
            {
                claims = claims.Concat(new[] { new Claim(ClaimTypes.Role, "User") });
            }
            else
            {
                claims = claims.Concat(new[] { new Claim(ClaimTypes.Role, "Admin") });
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
           var securityToken = new JwtSecurityToken(
               claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred
             );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<bool> GetUserAdminStatusAsync(string email)
        {
            var identityuser = await _userManager.FindByEmailAsync(email);
            if (identityuser is null)
            {
                return false;
            }
            return identityuser.IsAdmin;
            
        }

        public async Task<bool> Login(LoginModel user)
        {
            var identityuser = await _userManager.FindByEmailAsync(user.Username);
            if (identityuser is null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(identityuser, user.Password);
        }

        public async Task<bool> RegisterUser(RegisterModel user)
        {
            var identityUser = new ApplicationUser
            {
                UserName = user.Username,
                Email = user.Username,
                IsAdmin = user.IsAdmin,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                RefreshToken = "12wsftr",
                RefreshTokenExpiry = DateTime.Now,

            };
            var result = await _userManager.CreateAsync(identityUser, user.Password);
            return result.Succeeded;
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
        }
        public async Task<ApplicationUser> GetUser(string email)
        {
            var identityuser = await _userManager.FindByEmailAsync(email);
            return identityuser;
        }

        public async Task<RefreshResponse> RefreshToken(RefreshTokenModel model)
        {
            var principal = GetTokenPrincipal(model.AccessToken);
            var response = new RefreshResponse();

            if (principal?.Identity?.Name is null)
            {
                return response;
            }
            

            var identityUser = await _userManager.FindByNameAsync(principal?.Identity?.Name);

            if (identityUser is null || identityUser.RefreshToken != model.RefreshToken || identityUser.RefreshTokenExpiry <= DateTime.UtcNow)
            {
                return response;
            }

            response.Status = true;
            var username = identityUser.Email;
            LoginModel user = new LoginModel { Username = username };
            var accessToken = await GenerateTokenstringAsync(user);
            response.AccessToken = accessToken;
            return response;
            
        }

        private ClaimsPrincipal GetTokenPrincipal(string accessToken)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securitykey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
            try
            {
                return new JwtSecurityTokenHandler().ValidateToken(accessToken, validation, out _);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Token validation error: {ex.Message}");
                return null;
            }
        }
    }
    public interface IAuthService
    {
        string GenerateRefreshTokenString();
        Task<string> GenerateTokenstringAsync(LoginModel user);
        Task<bool> GetUserAdminStatusAsync(string email);

        Task<RefreshResponse> RefreshToken(RefreshTokenModel model);
        Task<bool> Login(LoginModel user);
        Task<bool> RegisterUser(RegisterModel user);

        Task UpdateUser(ApplicationUser user);

        Task<ApplicationUser> GetUser(string email);
    }
}
