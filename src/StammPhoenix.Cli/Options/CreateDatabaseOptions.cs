using CommandLine;
using JetBrains.Annotations;

namespace StammPhoenix.Cli.Options;

[PublicAPI]
[Verb("create")]
public class CreateDatabaseOptions : DatabaseOptionsBase;
