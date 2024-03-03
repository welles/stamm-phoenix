using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder();
builder.Services
   .AddFastEndpoints()
   .SwaggerDocument(d =>
   {
      d.DocumentSettings = s =>
      {
         s.Title = "Stamm Phoenix API";
         s.DocumentName = "v1";
      };
   });

var app = builder.Build();
app.UsePathBase("/api");
app.UseFastEndpoints(c =>
   {
      c.Endpoints.RoutePrefix = "api";
   })
   .UseSwaggerGen();
app.Run();
