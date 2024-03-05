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
using Nuke.Common.Tools.Npm;
using Nuke.Common.Tools.Pwsh;

[GitHubActions(
    nameof(Build.CompileApi),
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = new[]
    {
        "*"
    },
    FetchDepth = 0,
    InvokedTargets = new []
    {
        nameof(Build.CompileApi)
    }
)]
[GitHubActions(
    nameof(Build.CompileWeb),
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = new[]
    {
        "*"
    },
    FetchDepth = 0,
    InvokedTargets = new []
    {
        nameof(Build.CompileWeb)
    }
)]
[GitHubActions(
    nameof(Build.DockerPushDevApi),
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = new[]
    {
        "*"
    },
    FetchDepth = 0,
    InvokedTargets = new []
    {
        nameof(Build.DockerPushDevApi)
    },
    EnableGitHubToken = true,
    WritePermissions = new[] {
        GitHubActionsPermissions.Contents,
        GitHubActionsPermissions.Packages
    }
)]
[GitHubActions(
    nameof(Build.DockerPushDevWeb),
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = new[]
    {
        "*"
    },
    FetchDepth = 0,
    InvokedTargets = new []
    {
        nameof(Build.DockerPushDevWeb)
    },
    EnableGitHubToken = true,
    WritePermissions = new[] {
        GitHubActionsPermissions.Contents,
        GitHubActionsPermissions.Packages
    }
)]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.LogInfo);

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

    Project WebProject => Solution.Application.StammPhoenix_Web;

    Project CurrentProject;

    string CurrentDockerImageName;

    string DockerImageNameApi => IsLocalBuild ? "stamm-phoenix-api:dev" : "ghcr.io/welles/stamm-phoenix-api:dev";

    string DockerImageNameWeb => IsLocalBuild ? "stamm-phoenix-web:dev" : "ghcr.io/welles/stamm-phoenix-web:dev";

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
            Serilog.Log.Information($"Project = {CurrentProject}");
            Serilog.Log.Information($"DockerImageName = {CurrentDockerImageName}");
        });

    [PublicAPI]
    Target Clean => d => d
        .DependsOn(LogInfo)
        .Executes(() =>
        {
            DotNetTasks.DotNetClean(s => s.SetProject(ApiProject));
        });

    [PublicAPI]
    Target CompileApi => d=> d
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(s => s
                .SetVersion(MinVer.Version)
                .SetAssemblyVersion(MinVer.AssemblyVersion)
                .SetFileVersion(MinVer.FileVersion)
                .SetInformationalVersion(MinVer.Version)
                .SetProjectFile(ApiProject)
                .SetConfiguration(Configuration));
        });

    [PublicAPI]
    Target InstallBun => d => d
        .Executes(() =>
        {
            NpmTasks.NpmInstall(s => s
                .SetPackages("bun")
                .SetGlobal(true));
        });

    [PublicAPI]
    Target RestoreBunPackages => d => d
        .DependsOn(InstallBun)
        .Executes(() =>
        {
            PwshTasks.Pwsh(s => s
                .SetCommand("bun install")
                .SetProcessWorkingDirectory(WebProject.Directory));
        });

    [PublicAPI]
    Target CompileWeb => d => d
        .DependsOn(LogInfo, RestoreBunPackages)
        .Executes(() =>
        {
            PwshTasks.Pwsh(s => s
                .SetCommand("bun run build")
                .SetProcessWorkingDirectory(WebProject.Directory));
        });

    [PublicAPI]
    Target DockerBuild => d => d
        .DependsOn(LogInfo)
        .OnlyWhenDynamic(() => CurrentProject != null)
        .OnlyWhenDynamic(() => CurrentDockerImageName != null)
        .Executes(() =>
        {
            DockerTasks.DockerLogger = (_, e) => Serilog.Log.Information(e);

            DockerTasks.DockerImageBuild(s => s
                .SetBuildArg(
                    $"VERSION=\"{MinVer.Version}\"",
                    $"ASSEMBLY_VERSION=\"{MinVer.AssemblyVersion}\"",
                    $"FILE_VERSION=\"{MinVer.FileVersion}\"",
                    $"INFORMATIONAL_VERSION=\"{MinVer.Version}\"")
                .SetPath(CurrentProject.Directory)
                .SetTag(CurrentDockerImageName));
        });

    Target DockerBuildWeb => d => d
        .Executes(() =>
        {
            this.CurrentDockerImageName = DockerImageNameWeb;
            this.CurrentProject = WebProject;
        })
        .Triggers(DockerBuild);

    Target DockerBuildApi => d => d
        .Executes(() =>
        {
            this.CurrentDockerImageName = DockerImageNameApi;
            this.CurrentProject = ApiProject;
        })
        .Triggers(DockerBuild);

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
                .SetName(CurrentDockerImageName));
        });

    Target DockerPushDevWeb => d => d
        .Executes(() =>
        {
            this.CurrentDockerImageName = DockerImageNameWeb;
            this.CurrentProject = WebProject;
        })
        .Triggers(DockerPushDev);

    Target DockerPushDevApi => d => d
        .Executes(() =>
        {
            this.CurrentDockerImageName = DockerImageNameApi;
            this.CurrentProject = ApiProject;
        })
        .Triggers(DockerPushDev);
}
