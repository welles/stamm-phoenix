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
app.UseFastEndpoints()
   .UseSwaggerGen();
app.UsePathBase("/api");
app.Run();
