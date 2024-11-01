using Serilog;
using TicketsFilm.Service.IoC;

var builder = WebApplication.CreateBuilder(args);

SerilogConfigurator.ConfigureService(builder);
SwaggerConfigurator.ConfigureService(builder.Services);

var app = builder.Build();

SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);

//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.Run();