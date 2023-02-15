using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;


namespace UserLoginNew
{
    public class GetTokenJWT : IGetToken
    {
        public string Test { get; set; } = "testing";
        private readonly JWTSettings _options;
        public GetTokenJWT(IOptions<JWTSettings> optAccess)
        {
            _options = optAccess.Value;

        }

        public string TokenGet(string username)
        { 
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, username));
        claims.Add(new Claim("level", "123"));
        claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1)),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        );
                return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
