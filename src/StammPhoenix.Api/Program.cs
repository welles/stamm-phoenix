using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using Serilog;
using StammPhoenix.Api.Middlewares;
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

            c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
         })
         .UseSwaggerGen();

      app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
      app.UseMiddleware<GenericExceptionHandlingMiddleware>();

      await app.RunAsync();
   }
}
