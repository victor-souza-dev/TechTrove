﻿namespace Back.Models.Input;

public class UserInputCreate
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string UserName { get; private set; }

    public UserInputCreate(string email, string password, string userName)
    {
        Email = email;
        Password = password;
        UserName = userName;
    }
}

public class UserInputUpdate
{
    public string UserName { get; private set; }

    public UserInputUpdate(string userName)
    {
        UserName = userName;
    }
}
