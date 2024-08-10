using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BuberDinner.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator: IJwtTokenGenerator{

    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions){
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(User user){
        
#pragma warning disable CS8604 // Possible null reference argument.
        var signingCredentials = new SigningCredentials( 
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)
            ),
            SecurityAlgorithms.HmacSha256
        ) ;
#pragma warning restore CS8604 // Possible null reference argument.
        
        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiaryMinutes),
            claims: claims,
            signingCredentials: signingCredentials,
            audience: _jwtSettings.Audiance
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}