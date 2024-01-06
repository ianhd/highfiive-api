using Api.Repos;
using Api.Services.ThirdParty;
using Api.Services;
using Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
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
services.AddSingleton<PexelsService>();

services.AddCors();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
