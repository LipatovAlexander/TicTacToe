using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application;
using Application.Common.Interfaces;
using Infrastructure.Configurations.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public sealed class JwtGenerator : IJwtGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtGenerator(IOptions<JwtSettings> jwtSettingsOptions)
    {
        _jwtSettings = jwtSettingsOptions.Value;
    }

    public string GenerateToken(ApplicationUser user)
    {
        var issuer = _jwtSettings.Issuer;
        var audience = _jwtSettings.Audience;
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        var claims = CreateClaims(user);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }

    private static Claim[] CreateClaims(ApplicationUser user)
    {
        var claims = new List<Claim>();

        if (user.UserName is not null)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
        }

        return claims.ToArray();
    }
}