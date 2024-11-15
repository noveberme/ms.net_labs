using TicketsFilm.Service.IoC;

var builder = WebApplication.CreateBuilder(args);

DbContextConfigurator.ConfigureServices(builder);
SerilogConfigurator.ConfigureService(builder);
SwaggerConfigurator.ConfigureService(builder.Services);

var app = builder.Build();

DbContextConfigurator.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();

app.Run();