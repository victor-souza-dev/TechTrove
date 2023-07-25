using Back.Infra.Data;
using Back.Models.Entities;
using Back.Models.Input;
using Back.Services.Interfaces;

namespace Back.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<User> GetAll()
    {
        List<User> user = _context.User.Where(e => e.IsDeleted == false).ToList();
        return user;
    }

    public User GetById(Guid id)
    {
        var user = _context.User.FirstOrDefault(e => e.Id == id && e.IsDeleted == false);

        if (user == null)
        {
            throw new Exception("Usuário não existe");
        }

        return user;
    }


    public bool Created(User user)
    {
        _context.User.Add(user);
        _context.SaveChanges();
        return true;
    }

    public bool Update(Guid id, UserInputUpdate user)
    {
        var existingEmployee = _context.User.Find(id);

        if (existingEmployee == null)
        {
            throw new Exception("Usuário não existe");
        }

        existingEmployee.Updated(user.UserName);

        _context.Entry(existingEmployee).CurrentValues.SetValues(user);
        _context.SaveChanges();

        return true;
    }

    public bool Delete(Guid id)
    {
        var existingEmployee = _context.User.Find(id);

        if (existingEmployee == null)
        {
            throw new Exception("Usuário não existe");
        }

        existingEmployee.MarkAsDeleted();
        _context.SaveChanges();

        return true;
    }
}
