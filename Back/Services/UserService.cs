using Back.Infra.Data;
using Back.Models.Entities;
using Back.Models.Input;
using Back.Repositories.Interfaces;
using Back.Services.Interfaces;
using Back.Utils;

namespace Back.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    private readonly IAuthUser _authUser;
    private readonly HashedString _hashedString;
    public UserService(ApplicationDbContext context, IConfiguration config, IAuthUser authUser, HashedString hashedString)
    {
        _context = context;
        _config = config;
        _authUser = authUser;
        _hashedString = hashedString;
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
        string hashPassword = _hashedString.GetHashedPassword(user.Password);
        User formatUser = new User(user.Email, hashPassword, user.UserName);
        _context.User.Add(formatUser);
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

    public string Login(User user)
    {
        var validateUser = _authUser.ValidateUser(user);
        if (validateUser == null)
        {
            throw new Exception("Usuário inválido!");
        }
        string token = _authUser.GenerateToken(validateUser["Id"], validateUser["Email"], validateUser["UserName"]);
        return token;
    }

    public Dictionary<string, string> Me(string token)
    {
        var payload = _authUser.DecodingToken(token);
        return payload;
    }
}
