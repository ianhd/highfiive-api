using Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services.ThirdParty;

public class JwtService
{
    private readonly SiteSettings _siteSettings;

    public JwtService(SiteSettings siteSettings)
    {
        _siteSettings = siteSettings;
    }

    public string GenerateToken(User user)
    {
        var isDev = _siteSettings.DevEmailAddresses
            .Split(',')
            .ToList()
            .Exists(x => x == user.email);

        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_siteSettings.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim("id", user.user_id.ToString(), ClaimValueTypes.Integer),
            new Claim("name", user.name),
            new Claim("email", user.email),
            new Claim("picture", user.picture),
            new Claim("isDev", isDev.ToString(), ClaimValueTypes.Boolean)
        }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateToken(string token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_siteSettings.JwtSecret);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            // return user id from JWT token if validation successful
            return int.Parse(userId);
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }
}
