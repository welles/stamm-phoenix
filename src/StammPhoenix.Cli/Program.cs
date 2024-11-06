using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using StammPhoenix.Application;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Cli.Core;
using StammPhoenix.Cli.Options.Database;
using StammPhoenix.Cli.Options.Leaders;
using StammPhoenix.Infrastructure;
using StammPhoenix.Infrastructure.Configuration;

namespace StammPhoenix.Cli;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        if (args.Length == 0 || args.All(string.IsNullOrWhiteSpace))
        {
            var category = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select resource to handle:")
                    .AddChoices("Database", "Leaders")
            );

            switch (category)
            {
                case "Database":
                    await Program.HandleDatabaseCommands();
                    break;
                case "Leaders":
                    await Program.HandleLeadersCommands();
                    break;
            }

            return 0;
        }

        var result = Parser.Default.ParseArguments<CreateDatabaseOptions, UpdateDatabaseOptions>(
            args
        );

        var databaseOptions = result.Value as DatabaseOptionsBase;

        if (databaseOptions == null)
        {
            return 1;
        }

        var app = Program.GetAppFromArgs(databaseOptions);

        if (databaseOptions is CreateDatabaseOptions)
        {
            return await app.CreateDatase();
        }

        if (databaseOptions is UpdateDatabaseOptions)
        {
            return await app.UpdateDatabase();
        }

        if (databaseOptions is CreateLeaderOptions options)
        {
            return await app.CreateLeader(
                options.LoginEmail,
                options.FirstName,
                options.LastName,
                options.LoginPassword,
                options.Address,
                options.PhoneNumber
            );
        }

        return 255;
    }

    private static async Task HandleDatabaseCommands()
    {
        AnsiConsole.MarkupLine("[b]Handling Database[/]");

        var action = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("Select action:").AddChoices("Create", "Migrate")
        );

        await Task.CompletedTask; // TODO
        throw new NotImplementedException();
    }

    private static async Task HandleLeadersCommands()
    {
        AnsiConsole.MarkupLine("[b]Handling Leaders[/]");

        var action = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("Select action:").AddChoices("Create")
        );

        switch (action)
        {
            case "Create":
                await Program.CreateLeader();
                break;
        }
    }

    private static async Task CreateLeader()
    {
        AnsiConsole.MarkupLine("[b]Creating Leader[/]");

        var app = Program.GetAppFromRuntime();

        var loginEmail = AnsiConsole.Prompt(new TextPrompt<string>("Enter e-mail:"));
        var firstName = AnsiConsole.Prompt(new TextPrompt<string>("Enter first name:"));
        var lastName = AnsiConsole.Prompt(new TextPrompt<string>("Enter last name:"));
        var password = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:").Secret());
        var address = AnsiConsole.Prompt(new TextPrompt<string?>("Enter address:").AllowEmpty());
        if (string.IsNullOrWhiteSpace(address))
        {
            address = null;
        }
        var phoneNumber = AnsiConsole.Prompt(
            new TextPrompt<string?>("Enter phone number:").AllowEmpty()
        );
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            phoneNumber = null;
        }

        await app.CreateLeader(loginEmail, firstName, lastName, password, address, phoneNumber);
    }

    private static CliApp GetAppFromRuntime()
    {
        if (EnvironmentDatabaseConfiguration.IsValid)
        {
            AnsiConsole.MarkupLine("[b]DB connection was loaded from environment[/]");

            return Program.GetApp(new EnvironmentDatabaseConfiguration());
        }

        AnsiConsole.MarkupLine("[b]Enter DB connection parameters[/]");

        var host = AnsiConsole.Ask<string>("Enter host:");
        var port = AnsiConsole.Ask<int>("Enter port:");
        var database = AnsiConsole.Ask<string>("Enter database name:");
        var username = AnsiConsole.Ask<string>("Enter username:");
        var password = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:").Secret());

        var configuration = new CliDatabaseConfiguration(host, port, database, username, password);

        return Program.GetApp(configuration);
    }

    private static CliApp GetAppFromArgs(DatabaseOptionsBase options)
    {
        var configuration = new CliDatabaseConfiguration(options);

        return Program.GetApp(configuration);
    }

    private static CliApp GetApp(IDatabaseConfiguration configuration)
    {
        var services = new ServiceCollection()
            .AddSingleton<IDatabaseConfiguration>(configuration)
            .AddApplicationServices()
            .AddInfrastructureServices()
            .AddSingleton<ICurrentUser, CliCurrentUser>()
            .AddSingleton<CliApp>()
            .BuildServiceProvider();

        var app = services.GetRequiredService<CliApp>();

        return app;
    }
}
