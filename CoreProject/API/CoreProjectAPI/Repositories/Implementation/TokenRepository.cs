using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoreProjectAPI.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CoreProjectAPI.Repositories.Implementation;

public class TokenRepository(IConfiguration configuration): ITokenRepository
{
    public string CreateJwtToken(IdentityUser user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}