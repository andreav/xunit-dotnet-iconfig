using xunit_dotnet_iconfig.Config;

namespace xunit_dotnet_iconfig;

public class BaseTest
{
    protected readonly MyAppConfig _myAppConfig;

    public BaseTest()
    {
        _myAppConfig = TestConfigHelper.GetMyAppConfiguration();
    }
}