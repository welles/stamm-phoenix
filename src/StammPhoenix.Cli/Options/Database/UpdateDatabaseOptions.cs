using CommandLine;
using JetBrains.Annotations;

namespace StammPhoenix.Cli.Options.Database;

[PublicAPI]
[Verb("update")]
public class UpdateDatabaseOptions : DatabaseOptionsBase;
