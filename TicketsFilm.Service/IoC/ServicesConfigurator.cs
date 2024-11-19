using AutoMapper;
using TicketFilm.BL.Users.Manager;
using TicketFilm.BL.Users.Provider;
using TicketsFilm.DataAccess;
using TicketsFilm.DataAccess.Entities;
using TicketsFilm.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace TicketsFilm.Service.IoC;

public class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRepository<UserEntity>>(x => 
            new Repository<UserEntity>(x.GetRequiredService<IDbContextFactory<TicketsFilmDbContext>>()));
        services.AddScoped<IUsersProvider>(x => 
            new UsersProvider(x.GetRequiredService<IRepository<UserEntity>>(), 
                x.GetRequiredService<IMapper>()));
        services.AddScoped<IUsersManager>(x =>
            new UsersManager(x.GetRequiredService<IRepository<UserEntity>>(),
                x.GetRequiredService<IMapper>()));
}