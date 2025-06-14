using MyRoad.API;
using MyRoad.API.Middlewares;
using MyRoad.Domain;
using MyRoad.Infrastructure;
using MyRoad.Infrastructure.Persistence.config;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDomain()
    .AddInfrastructure(builder.Configuration, builder.Environment)
    .AddWeb(builder.Configuration);


builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));
builder.Host.UseSerilog((context, loggerConfig)
    => loggerConfig.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RateLimitingMiddleware>();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();