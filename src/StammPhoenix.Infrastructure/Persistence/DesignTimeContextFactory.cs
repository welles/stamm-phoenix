using FakeItEasy;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Design;
using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Infrastructure.Persistence;

[UsedImplicitly]
public class DesignTimeContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var passwordhasher = A.Fake<IPasswordHasher>();

        var currentUser = A.Fake<ICurrentUser>();

        var databaseConfiguration = new RuntimeDatabaseConfiguration
        {
            Host = string.Empty,
            Port = 5000,
            Database = string.Empty,
            Password = string.Empty,
            User = string.Empty
        };

        return new DatabaseContext(databaseConfiguration, [], passwordhasher, currentUser);
    }
}
