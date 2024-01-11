using Api.Services.ThirdParty;
using Microsoft.AspNetCore.Authorization;

namespace Api.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtService _jwtService;
    private readonly IWebHostEnvironment _env;

    public JwtMiddleware(RequestDelegate next, JwtService jwtService, IWebHostEnvironment env)
    {
        _next = next;
        _jwtService = jwtService;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        // if endpoint has [AllowAnonymous] OR Dev mode, allow
        if (context.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
        {
            await _next(context);
            return;
        }

        // validate everything else
        var authHeader = context.Request.Headers.Authorization;
        var authVal = authHeader.FirstOrDefault(); // expect "Bearer <token>"
        if (string.IsNullOrEmpty(authVal))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            throw new Exception("Not authorized");
        }

        // parse out jwt
        var jwt = authVal.Split(' ')[1];
        var userId = _jwtService.ValidateToken(jwt);

        if (!userId.HasValue)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            throw new Exception("Not authorized");
        }

        context.Items.Add("UserId", userId.Value);

        // authorized
        await _next(context);
    }

}
