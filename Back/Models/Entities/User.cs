namespace Back.Models.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string? UserName { get; private set; }
    public bool? IsDeleted { get; private set; } = false;
    public DateTime? UpdatedAt { get; private set; } = DateTime.UtcNow;

    public User(string email, string password, string? userName)
    {
        Email = email;
        Password = password;
        UserName = userName;
    }

    public void MarkAsDeleted()
    {
        UpdatedAt = DateTime.UtcNow;
        IsDeleted = true;
    }

    public void Updated(string userName)
    {
        UserName = userName;
        UpdatedAt = DateTime.UtcNow;
    }
}
