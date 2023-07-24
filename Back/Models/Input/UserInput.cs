namespace Back.Models.Input;

public class UserInputCreate
{
    public string Email { get; private set; }
    public string Password { get; private set; }

    public UserInputCreate(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

public class UserInputUpdate
{
    public string Email { get; private set; }
    public string Password { get; private set; }

    public UserInputUpdate(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
