namespace Back.Models.View;

public class UserView
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string UserName { get; private set; }

    public UserView(Guid id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }
}
