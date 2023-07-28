using Back.Models.Entities;

namespace Back.Repositories.Interfaces;

public interface IAuthUser
{
    string GenerateToken(string id, string email, string userName);
    Dictionary<string, string> DecodingToken(string token);
    Dictionary<string, string> ValidateUser(User user);
}
