using Xunit;
using Xunit.Abstractions;
using xunit_dotnet_iconfig.Config;

namespace xunit_dotnet_iconfig;

public class TestExample : BaseTest
{
    private readonly ITestOutputHelper output;

    public TestExample(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void Test1()
    {
        TestConfigHelper.ShowConfig(output);
        
        // Just to force output fro xUnit
        Assert.Equal(1,0);
    }
}