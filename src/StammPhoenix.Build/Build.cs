using System.Linq;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.MinVer;

[GitHubActions(
    nameof(Build.Compile),
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = new[]
    {
        "*"
    },
    FetchDepth = 0,
    InvokedTargets = new []
    {
        nameof(Build.Compile)
    }
)]
[GitHubActions(
    nameof(Build.DockerPushDev),
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = new[]
    {
        "*"
    },
    FetchDepth = 0,
    InvokedTargets = new []
    {
        nameof(Build.DockerPushDev)
    },
    EnableGitHubToken = true,
    WritePermissions = new[] {
        GitHubActionsPermissions.Contents,
        GitHubActionsPermissions.Packages
    }
)]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);

    [MinVer]
    readonly MinVer MinVer;

    [Parameter]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [CanBeNull]
    GitHubActions GitHubActions => GitHubActions.Instance;

    [GitRepository]
    readonly GitRepository GitRepository;

    [Solution(GenerateProjects = true)]
    readonly Solution Solution;

    Project ApiProject => Solution.Application.StammPhoenix_Api;

    string DockerImageName => IsLocalBuild ? "stamm-phoenix:dev" : "ghcr.io/welles/stamm-phoenix:dev";

    AbsolutePath Dockerfile => ApiProject.Directory / "Dockerfile";

    [PublicAPI]
    Target LogInfo => d=> d
        .Executes(() =>
        {
            Serilog.Log.Information($"MinVer.Version = {MinVer.Version}");
            Serilog.Log.Information($"MinVer.AssemblyVersion = {MinVer.AssemblyVersion}");
            Serilog.Log.Information($"MinVer.FileVersion = {MinVer.FileVersion}");
            Serilog.Log.Information($"IsLocalBuild = {IsLocalBuild}");
            Serilog.Log.Information($"Configuration = {Configuration}");
            Serilog.Log.Information($"Dockerfile = {Dockerfile}");
            Serilog.Log.Information($"GitHubActions.RepositoryOwner = {GitHubActions?.RepositoryOwner}");
            Serilog.Log.Information($"GitHubActions.ServerUrl = {GitHubActions?.ServerUrl}");
            Serilog.Log.Information($"DockerImageName = {DockerImageName}");
        });

    [PublicAPI]
    Target Restore => d => d
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetTasks.DotNetRestore();
        });

    [PublicAPI]
    Target Clean => d => d
        .DependsOn(LogInfo)
        .Executes(() =>
        {
            DotNetTasks.DotNetClean(s => s.SetProject(ApiProject));
        });

    [PublicAPI]
    Target Compile => d=> d
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(s => s
                .EnableNoRestore()
                .SetVersion(MinVer.Version)
                .SetAssemblyVersion(MinVer.AssemblyVersion)
                .SetFileVersion(MinVer.FileVersion)
                .SetInformationalVersion(MinVer.Version)
                .SetProjectFile(ApiProject)
                .SetConfiguration(Configuration));
        });

    [PublicAPI]
    Target DockerBuild => d => d
        .DependsOn(Compile)
        .Executes(() =>
        {
            DockerTasks.DockerLogger = (_, e) => Serilog.Log.Information(e);

            DockerTasks.DockerImageBuild(s => s
                .SetBuildArg(
                    $"VERSION=\"{MinVer.Version}\"",
                    $"ASSEMBLY_VERSION=\"{MinVer.AssemblyVersion}\"",
                    $"FILE_VERSION=\"{MinVer.FileVersion}\"",
                    $"INFORMATIONAL_VERSION=\"{MinVer.Version}\"")
                .SetPath(ApiProject.Directory)
                .SetTag(DockerImageName));
        });

    [PublicAPI]
    Target DockerLogin => d => d
        .DependsOn(DockerBuild)
        .OnlyWhenStatic(() => IsServerBuild)
        .Executes(() =>
        {
            DockerTasks.DockerLogin(s => s
                .SetServer("https://ghcr.io")
                .SetUsername(GitHubActions.RepositoryOwner)
                .SetPassword(GitHubActions.Token));
        });

    [PublicAPI]
    Target DockerPushDev => d => d
        .DependsOn(DockerLogin)
        .OnlyWhenStatic(() => IsServerBuild)
        .Executes(() =>
        {
            DockerTasks.DockerPush(s => s
                .SetName(DockerImageName));
        });
}
