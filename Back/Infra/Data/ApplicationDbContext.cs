using Back.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Back.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
}