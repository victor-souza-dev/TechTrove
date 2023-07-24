namespace Back.Models.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string UserName { get; private set; }
}
