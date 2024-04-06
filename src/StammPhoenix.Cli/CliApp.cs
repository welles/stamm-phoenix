using MediatR;
using Spectre.Console;
using StammPhoenix.Application.Commands.CreateDatabase;
using StammPhoenix.Application.Commands.CreateLeader;
using StammPhoenix.Application.Commands.MigrateDatabase;
using StammPhoenix.Cli.Options.Leaders;

namespace StammPhoenix.Cli;

public class CliApp
{
    private IMediator Mediator { get; }

    public CliApp(IMediator mediator)
    {
        this.Mediator = mediator;
    }

    public async Task<int> CreateDatase()
    {
        try
        {
            var success = await this.Mediator.Send(new CreateDatabaseCommand());

            return success ? 0 : 1;
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);

            return -1;
        }
    }

    public async Task<int> UpdateDatabase()
    {
        try
        {
            await this.Mediator.Send(new MigrateDatabaseCommand());

            return 0;
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);

            return -1;
        }
    }

    public async Task<int> CreateLeader(CreateLeaderOptions options)
    {
        try
        {
            var command = new CreateLeaderCommand
            {
                LoginEmail = options.LoginEmail,
                FirstName = options.FirstName,
                LastName = options.LastName,
                LoginPassword = options.LoginPassword,
                Address = options.Address,
                PhoneNumber = options.PhoneNumber
            };

            var leader = await this.Mediator.Send(command);

            AnsiConsole.MarkupLine($"[green]Leader {leader.FirstName} {leader.LastName} with ID {leader.Id} was created.[/]");

            return 0;
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);

            return -1;
        }
    }
}
