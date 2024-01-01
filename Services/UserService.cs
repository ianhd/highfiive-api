using Api.Repos;
using Api.Services.ThirdParty;
namespace Api.Services;

public class UserService
{
    private readonly UserRepo _userRepo;
    private readonly GoogleService _googleService;
    private readonly JwtService _jwtService;
    public UserService(UserRepo userRepo, GoogleService googleService, JwtService jwtService)
    {
        _userRepo = userRepo;
        _googleService = googleService;
        _jwtService = jwtService;
    }

    public async Task<string> Save(string accessToken)
    {
        var googleUser = await _googleService.GetUser(accessToken);
        var dbUser = await _userRepo.Save(googleUser);
        return _jwtService.GenerateToken(dbUser);
    }
}
