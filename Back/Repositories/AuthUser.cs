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

    public string GenerateToken(Guid id, string email, string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["JwtSettings.Key"]);
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
        };
        var token = tokenHandler.CreateToken(tokenConfig);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }

    public List<KeyValuePair<string, string>> ValidateUser(User user)
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

        return new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("Id", dbUser.Id.ToString()),
            new KeyValuePair<string, string>("Email", dbUser.Email),
            new KeyValuePair<string, string>("UserName", dbUser.UserName)
        };
    }
}
