using Back.Models.Entities;

namespace Back.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById();
        bool Created(User user);
        bool Update(Guid id, User user);
        bool Delete(Guid id);
    }
}
