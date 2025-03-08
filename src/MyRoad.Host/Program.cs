using MyRoad.API;
using MyRoad.Domain;
using MyRoad.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDomain()
    .AddInfrastructure()
    .AddWeb();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();