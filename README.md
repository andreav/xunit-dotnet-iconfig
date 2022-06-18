# Needing dotnet IConfiguration inside xUnit.net tests

* Create xunit project and packages
    
        dotnet new xunit --name xunit-dotnet-iconfig
        cd xunit-dotnet-iconfig
        dotnet add package Microsoft.Extensions.Configuration
        dotnet add package Microsoft.Extensions.Configuration.Binder
        dotnet add package Microsoft.Extensions.Configuration.Json
        dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
        dotnet add package Microsoft.Extensions.Configuration.UserSecrets        

* Create `appsetting.json` file (you can commit it, but without sensitive data). Here you put your config and placeholders for sensitive data)

* Create `appsetting.local.json` file (you must not commit it, .gitignore ). Here you put you sensitive data (overriding placeholders)  
_Note: In this project it is commited just for demostration purposes._

* Copy to output directory `appsetting.json` and `appsetting.local.json` by adding these lines to `.csproj` file:

    ```xml
    <ItemGroup Condition="'$(Configuration)' == 'Debug'">
        <None Update="appsettings.json" CopyToOutputDirectory="PreserveNewest" />
    </ItemGroup>
    <ItemGroup Condition="'$(Configuration)' == 'Debug'">
        <None Update="appsettings.local.json" CopyToOutputDirectory="PreserveNewest" />
    </ItemGroup>
    ```

* Create an Utilty class (`TestConfigHelper`) for setting up IConfiguration in every test and binding configuration to your config object (adjust as needed)

* Your test should inherit from a BaseTestClass which load configuration

* When debugging your code you will use your appsetting + appsetting.local

* When you deploy your code (i.e. to your CI/CD pipeline) you can grab sensitive data from environment
  You ca test this behavior also locally setting env vars inline when running tests, for instance like this (bash):

    ```          
    MyApp__Auth__TICKETOFFICE__UserName="override_secret" dotnet test
    ```

* Optionally you can configure a user secrets if managing more sensitive data. This requires some more configuration.
    * Creating anc setting a secret

        ```
        dotnet user-secrets init
        dotnet user-secrets set key value
        ```
    
    * Adding the secrets key to the `.csproj` file:

        ```xml
        <UserSecretsId>your-secret-id-here</UserSecretsId>
        ```

    * Configuring also Secrets in `TestConfigHelper.cs` (default code is commented out), something like this:

        ```cs
        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .AddUserSecrets("e3dfcccf-0cb3-423a-b302-e3e92e95c128")         // <-- this is the new line !!
                .AddEnvironmentVariables()
                .Build();
        }
        ```
