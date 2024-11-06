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

    public async Task<int> CreateLeader(
        string loginEmail,
        string firstName,
        string lastName,
        string password,
        string? address,
        string? phoneNumber
    )
    {
        try
        {
            var command = new CreateLeaderCommand
            {
                LoginEmail = loginEmail,
                FirstName = firstName,
                LastName = lastName,
                LoginPassword = password,
                Address = address,
                PhoneNumber = phoneNumber,
            };

            await AnsiConsole
                .Progress()
                .AutoClear(true)
                .StartAsync(async context =>
                {
                    var createTask = context
                        .AddTask("Creating leader...")
                        .IsIndeterminate()
                        .MaxValue(1);

                    var leader = await this.Mediator.Send(command);

                    createTask.Value(1);

                    AnsiConsole.MarkupLine(
                        $"[green]Leader {leader.FirstName} {leader.LastName} with ID {leader.Id} was created.[/]"
                    );
                });

            return 0;
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);

            return -1;
        }
    }
}
