using MyRoad.API;
using MyRoad.Domain;
using MyRoad.Infrastructure;
using MyRoad.Infrastructure.Persistence.config;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomain()
    .AddInfrastructure(builder.Configuration, builder.Environment)
    .AddWeb();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Host.UseSerilog((context, loggerConfig) 
    => loggerConfig.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();