using TicketsFilm.Service.IoC;

namespace TicketsFilm.Service.DI;

public class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        DbContextConfigurator.ConfigureServices(builder);
        SerilogConfigurator.ConfigureService(builder);
        //SwaggerConfigurator.ServicesConfigurator(builder.Services);
        MapperConfigurator.ConfigureServices(builder);
        ServicesConfigurator.ConfigureServices(builder.Services);
        
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