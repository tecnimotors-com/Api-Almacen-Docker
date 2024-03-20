using ApiAlmacen.Services.Interface;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ApiAlmacen.Services.Repo
{
    public class AuthService : IAuthService
    {
        public bool ValidateLogin(string user, string pass)
        {
            return true;
        }
        public string GenerateToken(DateTime date, string user, TimeSpan validDate, string Password)
        {
            var expire = date.Add(validDate);
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
            issuer: "asmdkasmdkosamdokjqnnoqwnoqnfoqnf",
            audience: "dsqonqwmovoqnfownoifqnoifnqowfn",
            claims: claims,
            notBefore: date,
            expires: expire,
            signingCredentials: signingCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
