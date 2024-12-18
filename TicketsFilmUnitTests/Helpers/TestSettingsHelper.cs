using TicketsFilm.Service.Settings;
namespace TestProject1.Helpers;

public class TestSettingsHelper
{
    public static TicketsFilmSettings GetSettings()
    {
        return TicketsFilmSettingsReader.Read(ConfigurationHelper.GetConfiguration());
    }
}