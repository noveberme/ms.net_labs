using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TicketsFilm.BL.Users.Manager;
using TicketsFilm.DataAccess;
using TicketsFilm.DataAccess.Entities;
using TicketsFilm.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using TicketFilm.BL.Authorization;
using TicketFilm.BL.Users.Provider;
using TicketsFilm.BL.Provider;
using TicketsFilm.Service.Settings;

namespace TicketsFilm.Service.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services, TicketsFilmSettings settings)
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
        
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IRepository<UserEntity>>(x =>
            new Repository<UserEntity>(x.GetRequiredService<IDbContextFactory<TicketsFilmDbContext>>()));
        services.AddScoped<IUsersProvider>(x =>
            new UsersProvider(x.GetRequiredService<IRepository<UserEntity>>(),
                x.GetRequiredService<IMapper>()));
        services.AddScoped<IUsersManager>(x =>
            new UsersManager(x.GetRequiredService<IRepository<UserEntity>>(),
                x.GetRequiredService<IMapper>()));

        services.AddScoped<IRepository<AdminEntity>>(x =>
            new Repository<AdminEntity>(x.GetRequiredService<IDbContextFactory<TicketsFilmDbContext>>()));

        services.AddScoped<IAuthProvider>(x =>
            new AuthProvider(x.GetRequiredService<SignInManager<UserEntity>>(),
                x.GetRequiredService<UserManager<UserEntity>>(),
                x.GetRequiredService<IHttpClientFactory>(),
                settings.IdentityServerUri!,
                settings.ClientId!,
                settings.ClientSecret!,
                x.GetRequiredService<IMapper>()
            ));
    }
}
