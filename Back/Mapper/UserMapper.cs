using AutoMapper;
using Back.Models.Entities;
using Back.Models.Input;
using Back.Models.View;

namespace Back.Mapper;

public class UserMapper: Profile
{
    public UserMapper()
    {
        CreateMap<UserInput, User>();
        CreateMap<User, UserView>();
    }
}
