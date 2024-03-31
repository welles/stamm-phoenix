using MediatR;
using Spectre.Console;
using StammPhoenix.Application.Commands.CreateDatabase;
using StammPhoenix.Application.Commands.MigrateDatabase;

namespace StammPhoenix.Cli;

public class CliApp
{
    public IMediator Mediator { get; }

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
}
