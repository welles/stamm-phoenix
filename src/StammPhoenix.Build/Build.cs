using Nuke.Common;

class Build : NukeBuild
{
    public static int Main () => NukeBuild.Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = NukeBuild.IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });

    Target Restore => _ => _
        .Executes(() =>
        {
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
        });

}
