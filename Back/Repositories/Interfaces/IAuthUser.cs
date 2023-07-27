using Back.Models.Entities;

namespace Back.Repositories.Interfaces;

public interface IAuthUser
{
    string GenerateToken(Guid id, string email, string userName);

    List<KeyValuePair<string, string>> ValidateUser(User user);
}
