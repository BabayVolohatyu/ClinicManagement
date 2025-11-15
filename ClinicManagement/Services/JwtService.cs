using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClinicManagement.Models.Auth;

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
                    new Claim("createdAt", user.CreatedAt.ToString("O")) // ISO 8601 format
                };

                // Add name claims
                if (!string.IsNullOrEmpty(user.MiddleName))
                    claims.Add(new Claim("middleName", user.MiddleName));

                if (!string.IsNullOrEmpty(user.LastName))
                    claims.Add(new Claim("lastName", user.LastName));

                // Add role and permissions
                if (user.Role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
                    claims.Add(new Claim("roleId", user.Role.Id.ToString()));

                    // Add individual permission claims
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
            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogWarning("Attempted to validate null or empty token");
                return null;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                if (!tokenHandler.CanReadToken(token))
                {
                    _logger.LogWarning("Invalid token format - cannot read token");
                    return null;
                }

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

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                _logger.LogDebug("JWT token validated successfully for user {UserId}",
                    principal.FindFirstValue("id"));

                return principal;
            }
            catch (SecurityTokenExpiredException)
            {
                _logger.LogWarning("JWT token has expired");
                return null;
            }
            catch (SecurityTokenInvalidIssuerException)
            {
                _logger.LogWarning("JWT token has invalid issuer");
                return null;
            }
            catch (SecurityTokenInvalidAudienceException)
            {
                _logger.LogWarning("JWT token has invalid audience");
                return null;
            }
            catch (SecurityTokenValidationException stvex)
            {
                _logger.LogWarning(stvex, "JWT token validation failed");
                return null;
            }
            catch (ArgumentException aex)
            {
                _logger.LogError(aex, "Argument error while validating JWT token");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while validating JWT token");
                return null;
            }
        }

        private void AddPermissionClaims(List<Claim> claims, Role role)
        {
            try
            {
                if (role.CanCreate) claims.Add(new Claim("permission", "create"));
                if (role.CanRead) claims.Add(new Claim("permission", "read"));
                if (role.CanUpdate) claims.Add(new Claim("permission", "update"));
                if (role.CanDelete) claims.Add(new Claim("permission", "delete"));
                if (role.CanExecuteRawQueries) claims.Add(new Claim("permission", "execute_raw_queries"));
                if (role.CanAskPromotion) claims.Add(new Claim("permission", "ask_promotion"));
                if (role.CanAcceptPromotions) claims.Add(new Claim("permission", "accept_promotions"));
                if (role.CanViewPromotionsList) claims.Add(new Claim("permission", "view_promotions_list"));
                if (role.CanManageUsers) claims.Add(new Claim("permission", "manage_users"));
                if (role.CanViewUserData) claims.Add(new Claim("permission", "view_user_data"));
                if (role.CanDownloadCsv) claims.Add(new Claim("permission", "download_csv"));

                _logger.LogDebug("Added {PermissionCount} permission claims for role {RoleName}",
                    claims.Count(c => c.Type == "permission"), role.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding permission claims for role {RoleName}", role.Name);
                throw;
            }
        }
    }
}