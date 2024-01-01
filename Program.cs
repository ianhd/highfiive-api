using Api;
using Api.Repos;
using Api.Services;
using Api.Services.ThirdParty;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// appsettings
var settings = new SiteSettings();
builder.Configuration.Bind("SiteSettings", settings);
services.AddSingleton(settings);

// singletons
services.AddSingleton<HashIdsService>();
services.AddSingleton<JwtService>();
services.AddSingleton<GoogleService>();
services.AddSingleton<DapperContext>();
services.AddSingleton<UserRepo>();
services.AddSingleton<UserService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", () =>
{
    return "Hiyooo. Check out /swagger for the endpoints";
});

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapPost("/user", async ([FromForm] UserPostRequest req, [FromServices]UserService userService) =>
{
    var jwt = await userService.Save(req.accessToken);
    return new
    {
        jwt
    };
});

//app.UseCors(x => x
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    .SetIsOriginAllowed(origin => true) // allow any origin
//    .AllowCredentials()); // allow credentials

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

internal record UserPostRequest(string accessToken);
