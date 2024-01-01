using Api.Models.Api;

namespace Api.Services.ThirdParty;

public class GoogleService
{
    public async Task<GoogleUser> GetUser(string accessToken)
    {
        using var http = new HttpClient();
        http.BaseAddress = new Uri("https://www.googleapis.com");
        return await http.GetFromJsonAsync<GoogleUser>($"/oauth2/v1/userinfo?access_token={accessToken}");
    }
}
