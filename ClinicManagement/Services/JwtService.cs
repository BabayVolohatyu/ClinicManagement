using ClinicManagement.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClinicManagement.Services
{
    public interface IJwtService
    {
        string Generate(User user);
        ClaimsPrincipal? Validate(string token);
    }

    public class JwtService : IJwtService
    {
        private readonly string _secureKey;
        private readonly IConfiguration _config;
        private readonly ILogger<JwtService> _logger;

        public JwtService(IConfiguration config, ILogger<JwtService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var key = config["JwtSettings:SecretKey"];

            if (string.IsNullOrWhiteSpace(key))
            {
                _logger.LogError("JWT secret key is null or empty in configuration");
                throw new ArgumentException("JWT secret key cannot be null or empty.");
            }

            _secureKey = key;
            _config = config;
        }

        public string Generate(User user)
        {
            if (user == null)
            {
                _logger.LogError("Attempted to generate JWT token for null user");
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                var claims = new List<Claim>
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("firstName", user.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("createdAt", user.CreatedAt.ToString("O")) 
                };

                
                if (!string.IsNullOrEmpty(user.MiddleName))
                    claims.Add(new Claim("middleName", user.MiddleName));

                if (!string.IsNullOrEmpty(user.LastName))
                    claims.Add(new Claim("lastName", user.LastName));

                
                if (user.Role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
                    claims.Add(new Claim("roleId", user.Role.Id.ToString()));

                    
                    AddPermissionClaims(claims, user.Role);
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var now = DateTime.UtcNow;
                var expirationMinutes = _config.GetValue<int>("JwtSettings:ExpirationMinutes", 60);

                var token = new JwtSecurityToken(
                    issuer: _config["JwtSettings:Issuer"],
                    audience: _config["JwtSettings:Audience"],
                    claims: claims,
                    expires: now.AddMinutes(expirationMinutes),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                _logger.LogInformation("JWT token generated successfully for user {UserId} with role {Role}",
                    user.Id, user.Role?.Name ?? "No Role");

                return tokenString;
            }
            catch (SecurityTokenException stex)
            {
                _logger.LogError(stex, "Security token error while generating JWT for user {UserId}", user.Id);
                throw new InvalidOperationException("Failed to generate security token", stex);
            }
            catch (ArgumentException aex)
            {
                _logger.LogError(aex, "Argument error while generating JWT for user {UserId}", user.Id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while generating JWT for user {UserId}", user.Id);
                throw;
            }
        }

        public ClaimsPrincipal? Validate(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(token)) return null;

                var key = Encoding.UTF8.GetBytes(_secureKey);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = !string.IsNullOrEmpty(_config["JwtSettings:Issuer"]),
                    ValidIssuer = _config["JwtSettings:Issuer"],
                    ValidateAudience = !string.IsNullOrEmpty(_config["JwtSettings:Audience"]),
                    ValidAudience = _config["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var validatedPrincipal = handler.ValidateToken(token, validationParameters, out _);

                var identity = new ClaimsIdentity(validatedPrincipal.Claims, authenticationType: "Jwt");
                return new ClaimsPrincipal(identity);
            }
            catch
            {
                return null;
            }
        }




        private void AddPermissionClaims(List<Claim> claims, Role role)
        {
            try
            {
                var permissions = new Dictionary<string, bool>
                {
                    ["create"] = role.CanCreate,
                    ["read"] = role.CanRead,
                    ["update"] = role.CanUpdate,
                    ["delete"] = role.CanDelete,
                    ["execute_raw_queries"] = role.CanExecuteRawQueries,
                    ["ask_promotion"] = role.CanAskPromotion,
                    ["accept_promotions"] = role.CanAcceptPromotions,
                    ["view_promotions_list"] = role.CanViewPromotionsList,
                    ["manage_users"] = role.CanManageUsers,
                    ["view_user_data"] = role.CanViewUserData,
                    ["download_csv"] = role.CanDownloadCsv
                };

                foreach (var p in permissions)
                {
                    if (p.Value)
                        claims.Add(new Claim("permission", p.Key));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding permission claims for role {RoleName}", role.Name);
                throw;
            }
        }
    }
}