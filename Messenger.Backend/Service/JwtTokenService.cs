using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Messenger.Backend.Entity;
using Messenger.Backend.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Messenger.Backend.Service;

public class JwtTokenService : IJwtTokenService
{
    private readonly string _secret;
    private readonly string _issuer;

    public JwtTokenService(string secret, string issuer)
    {
        _secret = secret ?? throw new ArgumentNullException(nameof(secret));
        _issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
    }

    public string GenerateToken(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrEmpty(user.Username)) throw new ArgumentException("User must have a username");
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _issuer,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}