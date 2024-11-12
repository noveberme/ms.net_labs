using TicketsFilm.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace TicketsFilm.Service.IoC;

public class DbContextConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();
        var connectionString = configuration.GetValue<string>("TicketsFilmContext");

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
