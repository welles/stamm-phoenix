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
    nameof(Build.DockerPushDevApi),
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = new[] { "main" },
    OnPushIncludePaths = new[]
    {
        "src/StammPhoenix.Build/**",
        "src/StammPhoenix.Api/**",
        "src/StammPhoenix.Domain/**",
    },
    JobConcurrencyGroup = nameof(Build.DockerPushDevApi),
    FetchDepth = 0,
    InvokedTargets = new[] { nameof(Build.DockerPushDevApi) },
    EnableGitHubToken = true,
    WritePermissions = new[]
    {
        GitHubActionsPermissions.Contents,
        GitHubActionsPermissions.Packages,
    }
)]
[GitHubActions(
    nameof(Build.DockerPushDevWeb),
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = new[] { "main" },
    OnPushIncludePaths = new[] { "src/StammPhoenix.Build/**", "src/StammPhoenix.Web/**" },
    JobConcurrencyGroup = nameof(Build.DockerPushDevWeb),
    FetchDepth = 0,
    InvokedTargets = new[] { nameof(Build.DockerPushDevWeb) },
    EnableGitHubToken = true,
    WritePermissions = new[]
    {
        GitHubActionsPermissions.Contents,
        GitHubActionsPermissions.Packages,
    }
)]
[GitHubActions(
    nameof(Build.DockerPushDevCli),
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = new[] { "main" },
    OnPushIncludePaths = new[] { "src/**" },
    JobConcurrencyGroup = nameof(Build.DockerPushDevCli),
    FetchDepth = 0,
    InvokedTargets = new[] { nameof(Build.DockerPushDevCli) },
    EnableGitHubToken = true,
    WritePermissions = new[]
    {
        GitHubActionsPermissions.Contents,
        GitHubActionsPermissions.Packages,
    }
)]
class Build : NukeBuild
{
    public static int Main() => Build.Execute<Build>(x => x.LogInfo);

    [MinVer]
    readonly MinVer MinVer;

    [Parameter]
    readonly Configuration Configuration = Build.IsLocalBuild
        ? Configuration.Debug
        : Configuration.Release;

    GitHubActions GitHubActions => GitHubActions.Instance;

    [GitRepository]
    readonly GitRepository GitRepository;

    [Solution(GenerateProjects = true)]
    readonly Solution Solution;

    Project ApiProject => Solution.Application.StammPhoenix_Api;

    Project WebProject => Solution.Application.StammPhoenix_Web;

    Project CliProject => Solution.Application.StammPhoenix_Cli;

    string DockerImageNameApi =>
        Build.IsLocalBuild ? "stamm-phoenix-api:dev" : "ghcr.io/welles/stamm-phoenix-api:dev";

    string DockerImageNameWeb =>
        Build.IsLocalBuild ? "stamm-phoenix-web:dev" : "ghcr.io/welles/stamm-phoenix-web:dev";

    string DockerImageNameCli =>
        Build.IsLocalBuild ? "stamm-phoenix-cli:dev" : "ghcr.io/welles/stamm-phoenix-cli:dev";

    string CurrentDockerImageName { get; set; }

    AbsolutePath CurrentDockerFile { get; set; }

    [PublicAPI]
    Target LogInfo =>
        d =>
            d.Executes(() =>
            {
                Serilog.Log.Information($"MinVer.Version = {MinVer.Version}");
                Serilog.Log.Information($"MinVer.AssemblyVersion = {MinVer.AssemblyVersion}");
                Serilog.Log.Information($"MinVer.FileVersion = {MinVer.FileVersion}");
                Serilog.Log.Information($"IsLocalBuild = {Build.IsLocalBuild}");
                Serilog.Log.Information($"Configuration = {Configuration}");
                Serilog.Log.Information($"Dockerfile = {CurrentDockerFile}");
                Serilog.Log.Information(
                    $"GitHubActions.RepositoryOwner = {GitHubActions?.RepositoryOwner}"
                );
                Serilog.Log.Information($"GitHubActions.ServerUrl = {GitHubActions?.ServerUrl}");
                Serilog.Log.Information($"DockerImageName = {CurrentDockerImageName}");
            });

    [PublicAPI]
    Target Clean =>
        d =>
            d.DependsOn(LogInfo)
                .Executes(() =>
                {
                    DotNetTasks.DotNetClean(s => s.SetProject(ApiProject));
                });

    [PublicAPI]
    Target CompileApi =>
        d =>
            d.DependsOn(Clean)
                .Executes(() =>
                {
                    DotNetTasks.DotNetBuild(s =>
                        s.SetVersion(MinVer.Version)
                            .SetAssemblyVersion(MinVer.AssemblyVersion)
                            .SetFileVersion(MinVer.FileVersion)
                            .SetInformationalVersion(MinVer.Version)
                            .SetProjectFile(ApiProject)
                            .SetConfiguration(Configuration)
                    );
                });

    [PublicAPI]
    Target GenerateApiClient =>
        d =>
            d.DependsOn(CompileApi)
                .Executes(() =>
                {
                    DotNetTasks.DotNetRun(s =>
                        s.SetProcessWorkingDirectory(ApiProject.Directory)
                            .SetProcessEnvironmentVariable("generateclients", "true")
                            .SetProcessEnvironmentVariable("LOG_PATH", ".")
                    );
                });

    [PublicAPI]
    Target ExecuteApi =>
        d =>
            d.DependsOn(CompileApi)
                .Executes(() =>
                {
                    DotNetTasks.DotNetRun(s =>
                        s.SetProcessWorkingDirectory(ApiProject.Directory)
                            .SetProcessEnvironmentVariable("LOG_PATH", @".")
                    );
                });

    [PublicAPI]
    Target InstallBun =>
        d =>
            d.Executes(() =>
            {
                NpmTasks.NpmInstall(s => s.SetPackages("bun").SetGlobal(true));
            });

    [PublicAPI]
    Target RestoreBunPackages =>
        d =>
            d.DependsOn(InstallBun)
                .Executes(() =>
                {
                    PwshTasks.Pwsh(s =>
                        s.SetCommand("bun install").SetProcessWorkingDirectory(WebProject.Directory)
                    );
                });

    [PublicAPI]
    Target CompileWeb =>
        d =>
            d.DependsOn(LogInfo, RestoreBunPackages)
                .Executes(() =>
                {
                    PwshTasks.Pwsh(s =>
                        s.SetCommand("bunx astro build")
                            .SetProcessWorkingDirectory(WebProject.Directory)
                    );
                });

    [PublicAPI]
    Target InstallPlaywright =>
        d =>
            d.DependsOn(LogInfo, CompileWeb)
                .Executes(() =>
                {
                    PwshTasks.Pwsh(s =>
                        s.SetCommand("bunx playwright install --with-deps")
                            .SetProcessWorkingDirectory(WebProject.Directory)
                    );
                });

    [PublicAPI]
    Target TestWeb =>
        d =>
            d.DependsOn(LogInfo, InstallPlaywright)
                .Executes(() =>
                {
                    PwshTasks.Pwsh(s =>
                        s.SetCommand("bunx playwright test")
                            .SetProcessWorkingDirectory(WebProject.Directory)
                    );
                });

    [PublicAPI]
    Target DockerBuild =>
        d =>
            d.DependsOn(LogInfo)
                .OnlyWhenDynamic(() => CurrentDockerFile != null)
                .OnlyWhenDynamic(() => CurrentDockerImageName != null)
                .Executes(() =>
                {
                    DockerTasks.DockerLogger = (_, e) => Serilog.Log.Information(e);

                    DockerTasks.DockerImageBuild(s =>
                        s.SetBuildArg(
                                $"VERSION=\"{MinVer.Version}\"",
                                $"ASSEMBLY_VERSION=\"{MinVer.AssemblyVersion}\"",
                                $"FILE_VERSION=\"{MinVer.FileVersion}\"",
                                $"INFORMATIONAL_VERSION=\"{MinVer.Version}\""
                            )
                            .SetPath(Solution.Directory)
                            .SetFile(CurrentDockerFile)
                            .SetTag(CurrentDockerImageName)
                    );
                });

    [PublicAPI]
    Target DockerBuildWeb =>
        d =>
            d.Executes(() =>
                {
                    CurrentDockerImageName = DockerImageNameWeb;
                    CurrentDockerFile = WebProject.Directory / "Dockerfile";
                })
                .Triggers(DockerBuild);

    [PublicAPI]
    Target DockerBuildApi =>
        d =>
            d.Executes(() =>
                {
                    CurrentDockerImageName = DockerImageNameApi;
                    CurrentDockerFile = ApiProject.Directory / "Dockerfile";
                })
                .Triggers(DockerBuild);

    [PublicAPI]
    Target DockerLogin =>
        d =>
            d.DependsOn(DockerBuild)
                .OnlyWhenStatic(() => Build.IsServerBuild)
                .Executes(() =>
                {
                    DockerTasks.DockerLogin(s =>
                        s.SetServer("https://ghcr.io")
                            .SetUsername(GitHubActions.RepositoryOwner)
                            .SetPassword(GitHubActions.Token)
                    );
                });

    [PublicAPI]
    Target DockerPushDev =>
        d =>
            d.DependsOn(DockerLogin)
                .OnlyWhenStatic(() => Build.IsServerBuild)
                .Executes(() =>
                {
                    DockerTasks.DockerPush(s => s.SetName(CurrentDockerImageName));
                });

    [PublicAPI]
    Target DockerPushDevWeb =>
        d =>
            d.DependsOn(TestWeb)
                .Executes(() =>
                {
                    CurrentDockerImageName = DockerImageNameWeb;
                    CurrentDockerFile = WebProject.Directory / "Dockerfile";
                })
                .Triggers(DockerPushDev);

    [PublicAPI]
    Target DockerPushDevApi =>
        d =>
            d.Executes(() =>
                {
                    CurrentDockerImageName = DockerImageNameApi;
                    CurrentDockerFile = ApiProject.Directory / "Dockerfile";
                })
                .Triggers(DockerPushDev);

    [PublicAPI]
    Target DockerPushDevCli =>
        d =>
            d.Executes(() =>
                {
                    CurrentDockerImageName = DockerImageNameCli;
                    CurrentDockerFile = CliProject.Directory / "Dockerfile";
                })
                .Triggers(DockerPushDev);
}
