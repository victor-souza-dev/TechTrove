using Back.Infra.Data;
using Back.Models.Entities;
using Back.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Back.Services;

public class UserService: IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<User> GetAll()
    {
       
    }

    public User GetById(Guid id)
    {
        
    }


    public bool Created(User employees)
    {
        
    }

    public bool Update(Guid id, User employees)
    {
        
    }

    public bool Delete(Guid id)
    {
        
    }
}
