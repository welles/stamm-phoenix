using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using StammPhoenix.Application;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Cli.Core;
using StammPhoenix.Cli.Options;
using StammPhoenix.Infrastructure;

namespace StammPhoenix.Cli;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var result = Parser.Default.ParseArguments<CreateDatabaseOptions, UpdateDatabaseOptions>(args);

        var databaseOptions = result.Value as DatabaseOptionsBase;

        if (databaseOptions == null)
        {
            return 1;
        }

        var app = Program.GetApp(databaseOptions);

        if (databaseOptions is CreateDatabaseOptions)
        {
            return await app.CreateDatase();
        }

        if (databaseOptions is UpdateDatabaseOptions)
        {
            return await app.UpdateDatabase();
        }

        return 255;
    }

    private static CliApp GetApp(DatabaseOptionsBase options)
    {
        var databaseConfiguration = new CliDatabaseConfiguration(options);

        var services = new ServiceCollection()
            .AddSingleton<IDatabaseConfiguration>(databaseConfiguration)
            .AddApplicationServices()
            .AddInfrastructureServices()
            .AddSingleton<CliApp>()
            .AddSingleton<ICurrentUser, CliCurrentUser>()
            .BuildServiceProvider();

        var app = services.GetRequiredService<CliApp>();

        return app;
    }
}
