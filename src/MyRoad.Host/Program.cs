using MyRoad.API;
using MyRoad.Domain;
using MyRoad.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomain()
    .AddInfrastructure(builder.Configuration)
    .AddWeb();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();