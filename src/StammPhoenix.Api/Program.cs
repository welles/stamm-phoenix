using FastEndpoints;
using FastEndpoints.Swagger;
using Serilog;
using StammPhoenix.Application;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Infrastructure;
using StammPhoenix.Infrastructure.Configuration;

namespace StammPhoenix.Api;

public static class Program
{
   public static readonly DateTime StartupTime = DateTime.UtcNow;

   public static async Task Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder();

      builder.Services
         .AddApplicationServices()
         .AddInfrastructureServices()
         .AddApiServices();

      var app = builder.Build();

      var appConfiguration = app.Services.GetRequiredService<IAppConfiguration>();

      app.UseCors(EnvironmentNames.ALLOWED_HOSTS);

      Log.Logger = new LoggerConfiguration()
         .WriteTo.Console()
         .WriteTo.File(Path.Combine(appConfiguration.LogPath, "log-.txt"), rollingInterval: RollingInterval.Day)
         .CreateLogger();

      app.UseSerilogRequestLogging();

      app.UseAuthentication()
         .UseAuthorization()
         .UseFastEndpoints(c =>
         {
            c.Endpoints.Configurator = ep =>
            {
               ep.DontAutoTag();
            };
         })
         .UseSwaggerGen();

      await app.RunAsync();
   }
}
