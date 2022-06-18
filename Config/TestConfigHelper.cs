using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace xunit_dotnet_iconfig.Config;

public static class TestConfigHelper
{
    public static IConfigurationRoot GetIConfigurationRoot()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
            .AddUserSecrets("e3dfcccf-0cb3-423a-b302-e3e92e95c128")
            .AddEnvironmentVariables()
            .Build();
    }

    public static MyAppConfig GetMyAppConfiguration()
    {
        var configuration = new MyAppConfig();
        GetIConfigurationRoot().Bind("MyApp", configuration);
        return configuration;
    }

    public static void ShowConfig(ITestOutputHelper output, IConfiguration? config = null)
    {
        config = config ?? GetIConfigurationRoot();
        foreach (var pair in config.GetChildren())
        {
            // Console.WriteLine($"{pair.Path} - {pair.Value}");
            output.WriteLine($"{pair.Path} - {pair.Value}");
            ShowConfig(output, pair);
        }
    }

}