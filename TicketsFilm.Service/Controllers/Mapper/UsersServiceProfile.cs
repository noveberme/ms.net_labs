using AutoMapper;
using TicketsFilm.BL.Users.Entity;
using TicketsFilm.Service.Controllers.Users.Entities;

namespace TicketsFilm.Service.Controllers.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<UserFilter, UserFilterModels>();
        CreateMap<RegisterUserRequest, CreateUserModel>();
    }
}