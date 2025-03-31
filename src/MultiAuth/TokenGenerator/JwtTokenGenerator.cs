using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MultiAuth.TokenGenerator;

public class JwtTokenGenerator
{
    public static string GenerateAzureUserJwt()
    {
        var securityKey = new SymmetricSecurityKey("uaouorzszljaailjftpgamsbiqwftkteccbzanugsramhubiysnmcceullobxatdwrrdcxmxmtwkpbqgdbqiwhubdyrcjspsfgqiaprmrlybfvvhfwxogfptawxvlgnh"u8.ToArray());
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[] 
        {
            new Claim(ClaimTypes.Role, "Admin"),   
            new Claim(ClaimTypes.Role, "Manager"),
            new Claim("iss", "https://login.microsoftonline.com"), // Azure issuer
            new Claim("sub", "user1"),
            new Claim("aud", "your_audience"),
            new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new Claim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds().ToString())
        };

        var jwt = new JwtSecurityToken(
            issuer: "https://login.microsoftonline.com",
            audience: "your_audience",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(jwt);
    }

    public static string GenerateSignInUserJwt()
    {
        var securityKey = new SymmetricSecurityKey("uaouorzszljaailjftpgamsbiqwftkteccbzanugsramhubiysnmcceullobxatdwrrdcxmxmtwkpbqgdbqiwhubdyrcjspsfgqiaprmrlybfvvhfwxogfptawxvlgnh"u8.ToArray());
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] 
        {
            new Claim("scope", "read"),
            new Claim("iss", "https://signin.company.com"), // SignIn issuer
            new Claim("sub", "user2"),
            new Claim("aud", "your_audience"),
            new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new Claim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds().ToString())
        };

        var jwt = new JwtSecurityToken(
            issuer: "https://signin.company.com",
            audience: "your_audience",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(jwt);
    }
}