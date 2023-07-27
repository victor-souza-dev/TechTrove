using AutoMapper;
using Back.Models.Entities;
using Back.Models.Input;
using Back.Models.View;

namespace Back.Mapper;

public class UserMapper: Profile
{
    public UserMapper()
    {
        CreateMap<UserInputLogin, User>();
        CreateMap<UserInputCreate, User>();
        CreateMap<User, UserView>();
    }
}
