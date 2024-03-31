using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using StammPhoenix.Application;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Cli.Core;
using StammPhoenix.Cli.Options;
using StammPhoenix.Infrastructure;

namespace StammPhoenix.Cli;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        DatabaseOptionsBase? databaseOptions = null;

        Parser.Default.ParseArguments<CreateDatabaseOptions, UpdateDatabaseOptions>(args)
            .WithParsed<CreateDatabaseOptions>(options => databaseOptions = options)
            .WithParsed<UpdateDatabaseOptions>(options => databaseOptions = options)
            .WithNotParsed(errors =>
            {
                // TODO
            });

        if (databaseOptions == null)
        {
            throw new InvalidOperationException();
        }

        var app = GetApp(databaseOptions);

        if (databaseOptions is CreateDatabaseOptions)
        {
            return await app.CreateDatase();
        }
        else if (databaseOptions is UpdateDatabaseOptions)
        {
            return await app.UpdateDatabase();
        }

        // TODO
        throw new NotImplementedException();
    }

    private static CliApp GetApp(DatabaseOptionsBase options)
    {
        var databaseConfiguration = new CliDatabaseConfiguration(options);

        var services = new ServiceCollection()
            .AddSingleton<IDatabaseConfiguration>(databaseConfiguration)
            .AddApplicationServices()
            .AddInfrastructureServices()
            .AddSingleton<CliApp>()
            .BuildServiceProvider();

        var app = services.GetRequiredService<CliApp>();

        return app;
    }
}
