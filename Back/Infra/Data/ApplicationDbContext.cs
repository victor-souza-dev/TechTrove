using Back.Infra.Data.OnModels;
using Back.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Back.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Shopping> Shopping { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.Ignore<Notification>();

        builder.ApplyConfiguration(new OnModelCreatingUser());
    }
}