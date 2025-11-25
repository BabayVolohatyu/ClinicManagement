using System.Collections.Concurrent;

namespace ClinicManagement.Services
{
    public interface IPasswordResetTokenService
    {
        string GenerateToken(string email);
        bool ValidateToken(string email, string token);
        void RemoveToken(string email);
    }

    public class PasswordResetTokenService : IPasswordResetTokenService
    {
        private readonly ConcurrentDictionary<string, TokenInfo> _tokens = new();
        private readonly TimeSpan _tokenExpiry = TimeSpan.FromHours(1);

        public string GenerateToken(string email)
        {
            var token = Guid.NewGuid().ToString("N");
            var tokenInfo = new TokenInfo
            {
                Token = token,
                Email = email,
                ExpiresAt = DateTimeOffset.UtcNow.Add(_tokenExpiry)
            };

            
            _tokens.TryRemove(email, out _);
            
            
            _tokens.TryAdd(email, tokenInfo);

            
            CleanupExpiredTokens();

            return token;
        }

        public bool ValidateToken(string email, string token)
        {
            if (!_tokens.TryGetValue(email, out var tokenInfo))
            {
                return false;
            }

            
            if (tokenInfo.Token != token || DateTimeOffset.UtcNow > tokenInfo.ExpiresAt)
            {
                _tokens.TryRemove(email, out _);
                return false;
            }

            return true;
        }

        public void RemoveToken(string email)
        {
            _tokens.TryRemove(email, out _);
        }

        private void CleanupExpiredTokens()
        {
            var expiredKeys = _tokens
                .Where(kvp => DateTimeOffset.UtcNow > kvp.Value.ExpiresAt)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in expiredKeys)
            {
                _tokens.TryRemove(key, out _);
            }
        }

        private class TokenInfo
        {
            public string Token { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public DateTimeOffset ExpiresAt { get; set; }
        }
    }
}
