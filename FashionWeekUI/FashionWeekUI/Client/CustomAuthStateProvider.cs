using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace KulinariumUI.Client;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
    {
        LocalStorage = localStorage;
        HttpClient = httpClient;
    }

    private HttpClient HttpClient { get; }
    private ILocalStorageService LocalStorage { get; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (States.Rendered)
        {
            var token = await LocalStorage.GetItemAsStringAsync("token");
            ClaimsIdentity identity = new();
            HttpClient.DefaultRequestHeaders.Authorization = null;

            if (string.IsNullOrEmpty(token).Equals(false))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromToken(token), "JWT");
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
                }

                catch
                {
                    await LocalStorage.RemoveItemsAsync(new[] { "token", "identifier" });
                    identity = new ClaimsIdentity();
                }
            }

            ClaimsPrincipal user = new(identity);
            AuthenticationState state = new(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        return new AuthenticationState(new ClaimsPrincipal());
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    private IEnumerable<Claim> ParseClaimsFromToken(string token)
    {
        var segment = token.Split('.').Skip(1).Take(1).Single();
        var bytes = ParseBase64WithoutPadding(segment);
        var pairs = JsonSerializer.Deserialize<Dictionary<string, object>>(bytes);
        var claims = pairs.Select(pair => new Claim(pair.Key, pair.Value.ToString()));

        return claims;
    }
}
