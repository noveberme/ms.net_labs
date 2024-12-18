using Microsoft.Extensions.Configuration;

namespace TestProject1.Helpers;

public class ConfigurationHelper
{
    public static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
    }
}