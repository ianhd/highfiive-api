//using Api.Repos;
using Api.Services.ThirdParty;
using Api.Services;
using Api;
using Api.Middleware;
using Microsoft.OpenApi.Models;
using Api.Models;
using Api.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Api.Repos;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

var currentAssembly = typeof(UserController).GetTypeInfo().Assembly;

services.AddControllers()
    .AddApplicationPart(currentAssembly);

services.AddEndpointsApiExplorer();
//services.AddSwaggerGen();
services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "HighFiive API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter jwt.",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

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
services.AddSingleton<BoardRepo>();
services.AddSingleton<BoardService>();
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

_ServiceLocator.Init(app.Services);
AppDependencyResolver.Init(app.Services);

app.UseHttpsRedirection();
// app.UseStaticFiles(); // for dark mode of swagger

app.UseMiddleware<JwtMiddleware>();
//app.UseAuthorization();

app.MapControllers();

app.Run();
