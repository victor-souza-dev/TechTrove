using AutoMapper;
using Back.Models.Entities;
using Back.Models.Input;
using Back.Models.View;

namespace Back.Mapper;

public class UserMapper: Profile
{
    public UserMapper()
    {
        CreateMap<UserInputCreate, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        CreateMap<UserInputUpdate, User>();
        CreateMap<User, UserView>();
    }
}
