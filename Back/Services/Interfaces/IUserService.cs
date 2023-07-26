using Back.Models.Entities;
using Back.Models.Input;

namespace Back.Services.Interfaces;

public interface IUserService
{
    string Login(User user);
    List<User> GetAll();
    User GetById(Guid id);
    bool Created(User user);
    bool Update(Guid id, UserInputUpdate user);
    bool Delete(Guid id);
}
