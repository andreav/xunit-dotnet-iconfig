using System.Collections.Generic;

namespace xunit_dotnet_iconfig.Config;

public class Site
{
    public string? BaseUrl { get; init; }
}

public class Login
{
    public string? UserName { get; init; }
    public string? Password { get; init; }

}

public class Auth
{
    public Login? INTERNAL { get; init; }
    public Login? TICKETOFFICE { get; init; }
}

public class MyAppConfig
{
    public Site? Site { get; init; }
    public Auth? Auth { get; init; }
}