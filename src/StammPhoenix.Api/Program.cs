using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Rewrite;

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
