using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace MinimalAPI_Project.JWTUtilites
{
    public class JwtTokenGenerator(string secretKey, string issuer, string audience)
    {
        private readonly string _secretKey = secretKey;
        private readonly string _issuer = issuer;
        private readonly string _audience = audience;

        public string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creadential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())//Jwt id, i can add more claims as needed
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                _audience,
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creadential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public ClaimsPrincipal Validatetoken(string token)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            var validateParamteres = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
            };
            try
            {
                var claimPrincipal = tokenHandle.ValidateToken(token, validateParamteres, out _);
                return claimPrincipal;
            }
            catch
            {
                Console.WriteLine("token Validation failed");
                return null;
            }
        }

    }
}
