using FastEndpoints;
using FastEndpoints.Swagger;

namespace StammPhoenix.Api;

public static class Program
{
   public static readonly DateTime StartupTime = DateTime.UtcNow;

   public static void Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder();
      builder.Services
         .AddFastEndpoints()
         .SwaggerDocument(d =>
         {
            d.DocumentSettings = s =>
            {
               s.Title = "Stamm Phoenix API";
               s.DocumentName = "current";
               s.Version = "Current";
            };
         });

      var app = builder.Build();
      app.UseFastEndpoints(c =>
         {
            c.Endpoints.Configurator = ep =>
            {
               ep.DontAutoTag();
            };
         })
         .UseSwaggerGen();

      app.Run();
   }
}
