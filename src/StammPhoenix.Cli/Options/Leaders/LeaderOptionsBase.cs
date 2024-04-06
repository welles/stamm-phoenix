using CommandLine;
using JetBrains.Annotations;
using StammPhoenix.Cli.Options.Database;

namespace StammPhoenix.Cli.Options.Leaders;

[PublicAPI]
[Verb("leaders")]
public abstract class LeaderOptionsBase : DatabaseOptionsBase;
