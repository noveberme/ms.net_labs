using TicketsFilm.Service.DI;
using TicketsFilm.Service.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var settings = TicketsFilmSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);

/*DbContextConfigurator.ConfigureServices(builder);
SerilogConfigurator.ConfigureService(builder);
SwaggerConfigurator.ConfigureService(builder.Services);

var app = builder.Build();

DbContextConfigurator.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();*/

ApplicationConfigurator.ConfigureServices(builder, settings);

var app = builder.Build();

ApplicationConfigurator.ConfigureApplication(app);


app.Run();