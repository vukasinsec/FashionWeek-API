using KulinariumUI.Client.Services.Interfaces;

namespace KulinariumUI.Client.Services;

public class AuthService : IAuthService
{
    public AuthService(HttpClient httpClient) => HttpClient = httpClient;

    private HttpClient HttpClient { get; }

    public Task<string> Register(string username, string password) => throw new NotImplementedException();

    public Task<string> LogIn(string username, string password) => throw new NotImplementedException();

    public Task<string> LogOut() => throw new NotImplementedException();
}

// TODO: Move All Auth Logic To AuthService
