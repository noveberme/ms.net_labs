﻿using TicketFilm.BL.Mapper;
using TicketsFilm.Service.Controllers.Mapper;

namespace TicketsFilm.Service.IoC;

public class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UsersBlProfile>();
            config.AddProfile<UsersServiceProfile>();
        });
    }
}