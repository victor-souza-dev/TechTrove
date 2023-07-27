using Back.Infra.Data;
using Back.Repositories.Interfaces;
using Back.Utils;
using Microsoft.EntityFrameworkCore;

namespace Back.Models.Input;

public class UserInputLogin
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string? UserName { get; private set; }

    public UserInputLogin(string email, string password)
    {
        Email = email;
        Password = password;
        UserName = SetUserNameFromEmail(email);
    }

    private string SetUserNameFromEmail(string email)
    {
        int atIndex = email.IndexOf('@');

        if (atIndex == -1)
        {
            return email;
        }
        else
        {
            string userName = email.Substring(0, atIndex);
            return $"{userName[0]}{userName.Substring(1)}";
        }
    }
}

public class UserInputCreate
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string UserName { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = DateTime.UtcNow;

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
