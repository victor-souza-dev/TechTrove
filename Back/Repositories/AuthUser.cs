using Back.Infra.Data;
using Back.Models.Entities;
using Back.Repositories.Interfaces;
using Back.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Back.Repositories;

public class AuthUser : IAuthUser
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    private readonly HashedString _hashedString;

    public AuthUser(ApplicationDbContext context, IConfiguration config, HashedString hashedString)
    {
        _context = context;
        _config = config;
        _hashedString = hashedString;
    }

    public Dictionary<string, string> DecodingToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JwtSettings:SecretKey"])),
            ValidateIssuer = true,
            ValidIssuer = _config["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = _config["JwtSettings:Audience"],
            ClockSkew = TimeSpan.Zero
        };

        SecurityToken validatedToken;
        var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

        var claims = principal.Claims;
        var dictionary = new Dictionary<string, string>();

        dictionary.Add("Id", principal.FindFirstValue("Id"));
        dictionary.Add("Email", principal.FindFirstValue("Email"));
        dictionary.Add("UserName", principal.FindFirstValue("UserName"));

        return dictionary;
    }

    public string GenerateToken(string id, string email, string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["JwtSettings:SecretKey"]);
        var issuer = _config["JwtSettings:Issuer"];
        var audience = _config["JwtSettings:Audience"];
        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", id.ToString()),
                new Claim("Email", email),
                new Claim("UserName", userName)
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Issuer = issuer,
            Audience = audience,
        };
        var token = tokenHandler.CreateToken(tokenConfig);

        return tokenHandler.WriteToken(token);
    }

    public Dictionary<string, string> ValidateUser(User user)
    {
        var dbUser = _context.User.FirstOrDefault(u => u.Email == user.Email);

        if (dbUser == null)
        {
            throw new Exception("Usuário não existe!");
        }

        var hashedPassword = _hashedString.GetHashedPassword(user.Password);

        if (dbUser.Password != hashedPassword)
        {
            throw new Exception(hashedPassword);
        }

        return new Dictionary<string, string>
        {
            { "Id", dbUser.Id.ToString() },
            { "Email", dbUser.Email },
            { "UserName", dbUser.UserName }
        };
    }
}
