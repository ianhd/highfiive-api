//using Api.Repos;
using Api;
using Api.Controllers;
using Api.Middleware;
using Api.Repos;
using Api.Services;
using Api.Services.ThirdParty;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddCors(o => o.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// I bet if we removed this part everything will still work
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

// repos
services.AddSingleton<UserRepo>();
services.AddSingleton<BoardRepo>();
services.AddSingleton<PostRepo>();

// services
services.AddSingleton<HashIdsService>();
services.AddSingleton<JwtService>();
services.AddSingleton<GoogleService>();
services.AddSingleton<DapperContext>();
services.AddSingleton<UserService>();
services.AddSingleton<BoardService>();
services.AddSingleton<PexelsService>();
services.AddSingleton<PostService>();
services.AddSingleton<GiphyService>();

var app = builder.Build();
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

_ServiceLocator.Init(app.Services);
AppDependencyResolver.Init(app.Services);

app.UseHttpsRedirection();
// app.UseStaticFiles(); // for dark mode of swagger

app.UseMiddleware<JwtMiddleware>();
//app.UseAuthorization();

app.MapControllers();

app.Run();
