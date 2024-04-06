using CommandLine;
using JetBrains.Annotations;

namespace StammPhoenix.Cli.Options.Database;

[PublicAPI]
[Verb("create")]
public record CreateDatabaseOptions : DatabaseOptionsBase;
