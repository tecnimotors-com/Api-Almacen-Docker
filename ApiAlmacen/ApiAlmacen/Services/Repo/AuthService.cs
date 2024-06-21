
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ApiAlmacen.Services.Interface;

namespace ApiAlmacen.Services.Repo
{
    public class AuthService(IConfiguration configuration) : IAuthService
    {
        private readonly IConfiguration configuration = configuration;

        public bool ValidateLogin(string user, string pass)
        {
            return true;
        }
        public string GenerateToken(DateTime date, string user, string Password)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(
                    JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64
                    ),
            };

            var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Password)),
            SecurityAlgorithms.HmacSha256Signature
            );
            var jwt = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AuthentificactionSettings:Issuer")!,
            audience: configuration.GetValue<string>("AuthentificactionSettings:Audience")!,
            claims: claims,
            notBefore: date,
            //expires: expire,
            signingCredentials: signingCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
