namespace TicketsFilm.Service.Settings;

public static class TicketsFilmSettingsReader
{
    public static TicketsFilmSettings Read(IConfiguration configuration)
    {
        return new TicketsFilmSettings
        {
            ConnectionString = configuration.GetValue<string>("TicketsFilmContext"),
            ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
            ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
            IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri")
        };
    }
}