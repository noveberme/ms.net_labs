using TicketsFilm.Service.IoC;
using TicketsFilm.Service.Settings;

namespace TicketsFilm.Service.DI;

public class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, TicketsFilmSettings settings)
    {
        AuthorizationConfigurator.ConfigureServices(builder.Services, settings);
        SerilogConfigurator.ConfigureService(builder);
        SwaggerConfigurator.ConfigureService(builder.Services);
        DbContextConfigurator.ConfigureServices(builder, settings);
        MapperConfigurator.ConfigureServices(builder.Services);
        ServicesConfigurator.ConfigureServices(builder.Services, settings);
        builder.Services.AddControllers();
    }
    public static void ConfigureApplication(WebApplication app)
    {
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);
        
        app.UseHttpsRedirection();
        app.MapControllers();
    }
}