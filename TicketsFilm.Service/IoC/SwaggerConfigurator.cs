﻿namespace TicketsFilm.Service.IoC;

public static class SwaggerConfigurator
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }


    public static void ConfigureApplication(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}