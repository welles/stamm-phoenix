using CommandLine;
using JetBrains.Annotations;

namespace StammPhoenix.Cli.Options;

[PublicAPI]
[Verb("update")]
public class UpdateDatabaseOptions : DatabaseOptionsBase;
