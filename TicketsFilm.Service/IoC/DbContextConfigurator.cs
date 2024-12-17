using Microsoft.EntityFrameworkCore;
using TicketsFilm.DataAccess;
using TicketsFilm.Service.Settings;

namespace TicketsFilm.Service.IoC;

public class DbContextConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, TicketsFilmSettings settings)
    {
        var connectionString = settings.ConnectionString;
        builder.Services.AddDbContextFactory<TicketsFilmDbContext>(
            options => { options.UseNpgsql(connectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<TicketsFilmDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}
